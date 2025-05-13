using MeetBriefly.Core.Interfaces;
using Azure;
using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using Azure.Messaging;

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

            var messages = new List<ChatMessageContext>
            {
                new MessageContent(ChatMessageRole.System, "Eres un asistente de reuniones profesional"),
                new ChatMessageContext(ChatMessageRole.User, prompt)
            };


            var response = await _client.GetChatCompletionsAsync("")
            
        }
    }
}
