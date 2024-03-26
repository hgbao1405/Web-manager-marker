using CharacterRepo.Data;
using CharacterRepo.Models;
using MediatR;
using Microsoft.VisualBasic;
using Shared.Archetype;
using Shared.Common;
using CharacterRepo.Models;

namespace CharacterRepo.Presentation.Command
{
    public class DeleteCharacterCommand : IRequest<Message>
    {
        public int Id { get; set; }

        public DeleteCharacterCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteCharacterHandle : IRequestHandler<DeleteCharacterCommand, Message>
    {
        public readonly MyWebContext _context;

        public DeleteCharacterHandle(MyWebContext context)
        {
            _context = context;
        }

        public async Task<Message> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = _context.Character.FirstOrDefault(x => x.id == request.Id);
            var mes = archetype.CheckAndDelete(character, "Nhân vật");

            if (mes.Error)
            {
                return mes;
            }
            _context.Character.Update(character);
            await _context.SaveChangesAsync(cancellationToken);

            mes.Title = "Xóa thành công";

            return mes;
        }
    }
}