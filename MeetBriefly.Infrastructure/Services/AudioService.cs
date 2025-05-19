using MeetBriefly.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Infrastructure.Services
{
    public class AudioService : IAudioService
    {
        private readonly string _azureApiKey;
        private readonly IAudioConverter _audioConverter;
        private readonly IConfiguration _configuration;

        public AudioService(IAudioConverter audioConverter, IConfiguration configuration)
        {
            _audioConverter = audioConverter;
            _configuration = configuration;
            _azureApiKey = _configuration["AzureAudioApiKey"]!.ToString();
        }

        public async Task<string> TranscribeAudioAsync(IFormFile audioFile)
        {
            var speechConfig = SpeechConfig.FromEndpoint(new Uri("https://eastus.api.cognitive.microsoft.com/"), _azureApiKey);
            speechConfig.SpeechRecognitionLanguage = "es-CR";

            var tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.wav");

            await using var wavStream = await ConvertToWavAsync(audioFile);
            await using var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write);
            await wavStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            wavStream.Dispose();

            var audioConfig = AudioConfig.FromWavFileInput(tempPath);

            var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var result = await recognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                return result.Text;
            }

            return "Error Transcribing";
        }


        private async Task<Stream> ConvertToWavAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            using var inputStream = file.OpenReadStream();

            switch (extension)
            {
                case ".mp3":
                    return _audioConverter.ConvertMp3ToWav(inputStream);

                case ".wav": 
                    var memoryStream = new MemoryStream();
                    await inputStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    return memoryStream;
                //to do add more formats
                default:
                    throw new NotSupportedException($"Extension not supported  {extension}");
            }
        }

    }
}
