using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CharacterRepo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using CharacterRepo.Presentation.Query;
using CharacterRepo.ModelView;
using Shared.Common;
using CharacterRepo.Models;
using CharacterRepo.Presentation.Command;

namespace CharacterModule.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        public readonly IMediator _mediator;
        public CharactersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<List<CharacterDTO>> GetAllCharacter(string? keyWord)
        {
            List<CharacterDTO> characters = await _mediator.Send(new GetAllCharacterQuery(keyWord));
            return characters;
        }

        [HttpGet]
        public async Task<List<BackgroundDTO>> GetAllBackGround(string? keyWord)
        {
            var characters = await _mediator.Send(new GetAllBackGroundQuery(keyWord));
            return characters;
        }
        [HttpPost]
        public async Task<Character> AddCharacter([FromBody] CharacterDTO character)
        {
            var characters = await _mediator.Send(new AddCharacterCommand(character));
            return characters;
        }
        [HttpDelete]
        public async Task<Message> DeleteCharacter(int id)
        {
            var res=await _mediator.Send(new DeleteMarkerCommand(id));
            return res;
        }
    }
}
