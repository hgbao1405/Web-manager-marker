using CharacterRepo.Data;
using CharacterRepo.Models;
using MediatR;
using Microsoft.VisualBasic;
using Shared.Archetype;
using Shared.Common;

namespace CharacterRepo.Presentation.Command
{
    public class DeleteMarkerCommand : IRequest<Message>
    {
        public int Id { get; set; }

        public DeleteMarkerCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteMarkerHandle : IRequestHandler<DeleteMarkerCommand, Message>
    {
        public readonly MyWebContext _context;

        public DeleteMarkerHandle(MyWebContext context)
        {
            _context = context;
        }

        public async Task<Message> Handle(DeleteMarkerCommand request, CancellationToken cancellationToken)
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