using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTranslationAIAgent.Interface;

namespace SimpleTranslationAIAgent.ViewModels.Pages
{
    public partial class AIChatViewModel : ObservableObject
    {
        private readonly ISemanticKernelService _semanticKernelService;
        public AIChatViewModel(ISemanticKernelService semanticKernelService)
        {
            _semanticKernelService = semanticKernelService;
        }

        [ObservableProperty]
        private string askText = "";

        [ObservableProperty]
        private string responseText = "";

        [ObservableProperty]
        private Visibility progressRingVisible = Visibility.Hidden;

        [RelayCommand]
        private async Task AskAI()
        {
            if (ResponseText != "")
            {
                ResponseText = "";
            }
            if (AskText != "")
            {
                ProgressRingVisible = Visibility.Visible;
                await foreach (var chunk in _semanticKernelService.GetAIResponse2(AskText))
                {
                    ResponseText += chunk;
                }
                ProgressRingVisible = Visibility.Hidden;           
            }
            else
            {
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "WPF UI Message Box",
                    Content =
                    "请先输入问题！",
                };

                _ = await uiMessageBox.ShowDialogAsync();
            }

        }


    }
}
