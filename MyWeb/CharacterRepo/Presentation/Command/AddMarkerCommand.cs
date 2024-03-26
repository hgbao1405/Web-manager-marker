using CharacterRepo.Data;
using CharacterRepo.Models;
using CharacterRepo.ModelView;
using CharacterRepo.Presentation.Query;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shared.Common;

namespace CharacterRepo.Presentation.Command
{
    public class AddMarkerCommand : IRequest<Marker>
    {
        public MarkerDTO character;

        public AddMarkerCommand(MarkerDTO character)
        {
            this.character = character;
        }
    }
    public class AddMarkerHandle : IRequestHandler<AddMarkerCommand, Marker>
    {
        private readonly MyWebContext _context;

        public AddMarkerHandle(MyWebContext context)
        {
            _context = context;
        }

        public async Task<Marker> Handle(AddMarkerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var c = new Marker
                {
                    x = request.character.x,
                    y = request.character.y,
                    z = request.character.z,
                    dgr = request.character.dgr,
                    typeid = request.character.typeid
                };

                _context.Add(c);
                await _context.SaveChangesAsync(cancellationToken);
                return c;
            }
            catch(Exception ex) {
                throw ex;
            }
        }
    }
}