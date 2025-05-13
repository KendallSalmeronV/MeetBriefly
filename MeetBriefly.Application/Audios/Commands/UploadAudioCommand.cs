using MediatR;
using MeetBriefly.Core.DTOS;
using MeetBriefly.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Application.Audios.Commands
{
    public class UploadAudioCommand :IRequest<SummaryDTO>
    {
        public IFormFile AudioFile { get; set; }

        public UploadAudioCommand(IFormFile audioFile)
        {
            AudioFile = audioFile;
        }

    }

    public class UploadAudioCommanHandler : IRequestHandler<UploadAudioCommand, SummaryDTO>
    {
        private readonly IAudioService _audioService;
        private readonly ITextSumarizationService _textSumarizationService;
        public UploadAudioCommanHandler(IAudioService audioService, ITextSumarizationService textSumarizationService)
        {
            _audioService = audioService;
            _textSumarizationService = textSumarizationService;
        }

        public async Task<SummaryDTO> Handle(UploadAudioCommand request, CancellationToken cancellationToken)
        {
            var transcription = await _audioService.TranscribeAudioAsync(request.AudioFile);

            var summary = await _textSumarizationService.SumarizeTextAsync(transcription);

            //extract tasks

            return new SummaryDTO
            {
                Summary = summary,
                Tasks = new List<string>()
            };
        }
    }
}
