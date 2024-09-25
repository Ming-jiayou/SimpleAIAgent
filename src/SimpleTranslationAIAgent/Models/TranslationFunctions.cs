using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleTranslationAIAgent.Common.Options;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.SemanticKernel.Text;

namespace SimpleTranslationAIAgent.Models
{
#pragma warning disable SKEXP0050
    internal class TranslationFunctions
    {
        private readonly Kernel _kernel;
        public TranslationFunctions()
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            _kernel = builder.Build();
        }
        [KernelFunction, Description("选择用户想要的语言翻译文本")]
        public async Task<string> TranslateText(
            [Description("要翻译的文本")] string text,
            [Description("要翻译成的语言，从'中文'、'英文'中选一个")] string language
 )
        {
            string skPrompt = """
                            {{$input}}

                            将上面的文本翻译成{{$language}}，无需任何其他内容
                            """;
            var result = await _kernel.InvokePromptAsync(skPrompt, new() { ["input"] = text, ["language"] = language });
            var str = result.ToString();
            return str;
        }

        [KernelFunction, Description("实现文件到文件的翻译")]
        public async Task<string> TranslateTextFileToFile(
           [Description("要翻译的文件路径")] string path1,
           [Description("保存翻译结果的文件路径")] string path2,
           [Description("要翻译成的语言，从'中文'、'英文'中选一个")] string language
)
        {
            string fileContent = File.ReadAllText(path1);
            var lines = TextChunker.SplitPlainTextLines(fileContent,100);
            var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 1000);
            string result = "";
            string skPrompt = """
                            {{$input}}

                            将上面的文本翻译成{{$language}}，无需任何其他内容
                            """;
            foreach (var paragraph in paragraphs)
            {
                var result1 = await _kernel.InvokePromptAsync(skPrompt, new() { ["input"] = paragraph, ["language"] = language });
                result += result1.ToString() + "\r\n";
            }        
           
            var str = result.ToString();

            // 使用 StreamWriter 将文本写入文件
            using (StreamWriter writer = new StreamWriter(path2, true))
            {
                writer.WriteLine(str);
            }

            string message = $"已成功实现文件{path1}到文件{path2}的翻译";
            return message;
        }

        [KernelFunction, Description("将文本保存到文件")]
        public string SaveTextToFile(
           [Description("要保存的文本")] string text,
           [Description("要保存到的文件路径")] string filePath
)
        {
            // 使用 StreamWriter 将文本写入文件
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(text);
            }
            return "已成功写入文件";
        }

        [KernelFunction, Description("从文件中读取文本")]
        public string GetTextFromFile(
           [Description("要读取的文件路径")] string filePath
)
        {
            string fileContent = File.ReadAllText(filePath);
            return fileContent;
        }

    }
}
