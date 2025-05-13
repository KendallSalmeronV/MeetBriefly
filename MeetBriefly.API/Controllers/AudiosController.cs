using MediatR;
using MeetBriefly.Application.Audios.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MeetBriefly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudiosController : ControllerBase
    {
        private IMediator _mediator;

        public AudiosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAudio([FromForm] IFormFile audioFile)
        {
            try
            {
                var command = new UploadAudioCommand(audioFile);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
