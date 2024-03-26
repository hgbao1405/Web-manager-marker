using CharacterRepo.Models;
using CharacterRepo.Presentation.Command;
using CharacterRepo.Presentation.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CharacterModule.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MarkerController : ControllerBase
    {
        public readonly IMediator _mediator;
        public MarkerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<TypeMaker>> GetAllTypeMarker()
        {
            var types = await _mediator.Send(new GetAllTypeMarkerQuery());
            return types;
        }
        [HttpGet]
        public async Task<List<MarkersModelView>> GetAllMarker(string? keyWord)
        {
            var characters = await _mediator.Send(new GetAllMarkerQuery(keyWord));
            return characters;
        }
        [HttpPost]
        public async Task<Marker> AddMarker([FromBody] MarkerDTO character)
        {
            var characters = await _mediator.Send(new AddMarkerCommand(character));
            return characters;
        }
        [HttpDelete]
        public async Task<Message> DeleteMarker(int id)
        {
            var res = await _mediator.Send(new DeleteMarkerCommand(id));
            return res;
        }
    }
}
