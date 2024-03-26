using CharacterRepo.Data;
using CharacterRepo.Models;
using CharacterRepo.ModelView;
using MediatR;

namespace CharacterRepo.Presentation.Command
{
    public class AddCharacterCommand : IRequest<Character>
    {
        public CharacterDTO character;

        public AddCharacterCommand(CharacterDTO character)
        {
            this.character = character;
        }
    }
    public class AddCharacterHandle : IRequestHandler<AddCharacterCommand, Character>
    {
        private readonly MyWebContext _context;

        public AddCharacterHandle(MyWebContext context)
        {
            _context = context;
        }

        public async Task<Character> Handle(AddCharacterCommand request, CancellationToken cancellationToken)
        {
            Character c = Character.Convert(request.character);
            _context.Add(c);
            await _context.SaveChangesAsync(cancellationToken);
            return c;
        }
    }
}