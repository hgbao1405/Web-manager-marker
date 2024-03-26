using CharacterRepo.Data;
using CharacterRepo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharacterRepo.Presentation.Query
{
    public class GetAllTypeMarkerQuery : IRequest<List<TypeMaker>>
    {
    }
    public class GetAllTypeMarkerHandel : IRequestHandler<GetAllTypeMarkerQuery, List<TypeMaker>>
    {
        private readonly MyWebContext _context;

        public GetAllTypeMarkerHandel(MyWebContext context)
        {
            _context = context;
        }

        public async Task<List<TypeMaker>> Handle(GetAllTypeMarkerQuery request, CancellationToken cancellationToken)
        {
            return await _context.TypeMakers.ToListAsync();
        }
    }
}