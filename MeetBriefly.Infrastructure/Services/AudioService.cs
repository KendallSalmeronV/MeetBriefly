using MeetBriefly.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Infrastructure.Services
{
    public class AudioService : IAudioService
    {
        private readonly string _azureApiKey = "1lRfp6xox9HJxtepLmWs1y56ZH2DB5O9VNQPR2QoMBEzXHyoFBcuJQQJ99BEACYeBjFXJ3w3AAAYACOGY5Yo";
        private readonly string _azureRegion = "eastus";

        public async Task<string> TranscribeAudioAsync(IFormFile audioFile)
        {

            var speechConfig = SpeechConfig.FromEndpoint(new Uri("https://eastus.api.cognitive.microsoft.com/"), _azureApiKey);
            speechConfig.SpeechRecognitionLanguage = "es-CR";

            var tempPath = Path.GetTempFileName();
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await audioFile.CopyToAsync(stream);
            }

            var audioConfig = AudioConfig.FromWavFileInput(tempPath);


            var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var result = await recognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                return result.Text;
            }

            return "Error Transcribing";
        }

    }
}
