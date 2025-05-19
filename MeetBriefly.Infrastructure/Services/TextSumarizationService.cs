using MeetBriefly.Core.Interfaces;
using Azure;
using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using Azure.Messaging;
using OpenAI.Chat;
using System.Text.Json;
using MeetBriefly.Core.Entities;

namespace MeetBriefly.Infrastructure.Services
{
    public class TextSumarizationService : ITextSumarizationService
    {
        private readonly string _azureApiKey = "F8RnDztLd4nPRjgOE3TbBs9aDB4y5aNw0JzWMPiBEi089M8nbN4iJQQJ99BEACYeBjFXJ3w3AAABACOGJG4e";
        private readonly string _region = "eastus";
        private readonly string _language = "es";
        private readonly string _endpoint = "https://meetbriefly-openai.openai.azure.com/";
        private readonly AzureOpenAIClient _client;
        private readonly string _deployment = "meetbriefly-gpt-4o";


        public TextSumarizationService()
        {
            _client = new AzureOpenAIClient(new Uri(_endpoint), new AzureKeyCredential(_azureApiKey));
        }
        public async Task<string> SumarizeTextAsync(string text)
        {
            string prompt = $@"
                Actúa como asistente de reuniones. Del texto a continuación generame:
                    - Un resumen breve
                    - Lista de decisiones tomadas
                    - lista de Tareas con responsables (si se mencionan)
                    - Nombres Clave de personas mencionadas

                Texto:
                ""{ text}""
            ";

            var chatClient = _client.GetChatClient("meetbriefly-gpt-4o");

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(prompt)
            };
            var chatCompletionsOptions = new ChatCompletionOptions
            {
                Temperature = (float)0.7,
                MaxOutputTokenCount=800,
                TopP = (float)0.95,
                FrequencyPenalty = (float)0,
                PresencePenalty = (float)0,

            };

            try
            {
                var completion = await chatClient.CompleteChatAsync(messages, options: chatCompletionsOptions);
                if (completion != null)
                {
                    return completion.Value.Content[0].Text;
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }   

        }
    }
}
