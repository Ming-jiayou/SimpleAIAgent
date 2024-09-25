using SimpleTranslationAIAgent.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTranslationAIAgent.Interface
{
    public interface ISemanticKernelService
    {
        public Task<Query> GetAIResponse(string question);
        public IAsyncEnumerable<string> GetAIResponse2(string question);
        public Task<string> RunUniversalLLMFunctionCallerSampleAsync(string askText);
        public Task<string> RunTranslationAIAgentSampleAsync(string askText);
    }
}
