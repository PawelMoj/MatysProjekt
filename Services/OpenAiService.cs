using Azure.Core;
using MatysProjekt.Configurations;
using Microsoft.Extensions.Options;
using OpenAI_API;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MatysProjekt.Services
{
    public class OpenAiService : IOpenAiService 
    {
        private readonly OpenAiConfig _openAiConfig;

        public OpenAiService( IOptionsMonitor<OpenAiConfig> optionsMonitor) {
            _openAiConfig = optionsMonitor.CurrentValue;
        }
        public async Task<string> CopleteSentence(string text)
        { 
            //api instance
            var openAi = new OpenAIAPI(_openAiConfig.Key);

            var conversation = openAi.Chat.CreateConversation();
            conversation.AppendUserInput(text);
            try 
            {
                var respose = await conversation.GetResponseFromChatbotAsync();
                return respose.ToString();
            }
            catch { return "dupa"; }
            

        }
    }
}
