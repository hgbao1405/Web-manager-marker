using CharacterRepo.Data;
using CharacterRepo.Models;
using CharacterRepo.ModelView;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharacterRepo.Presentation.Query
{
    public class GetAllCharacterQuery : IRequest<List<CharacterDTO>>
    {
        public string keyWord { get; set; }

        public GetAllCharacterQuery(string keyWord)
        {
            this.keyWord = keyWord;
        }
    }
    public class GetAllCharacterHandel : IRequestHandler<GetAllCharacterQuery, List<CharacterDTO>>
    {
        private readonly MyWebContext _context;

        public GetAllCharacterHandel(MyWebContext context)
        {
            _context = context;
        }

        public async Task<List<CharacterDTO>> Handle(GetAllCharacterQuery request, CancellationToken cancellationToken)
        {
            var list = await (from x in _context.Character.Where(x=> x.IsDeleted != true)
                              where (string.IsNullOrEmpty(request.keyWord) || x.name.Contains(request.keyWord) || x.description.Contains(request.keyWord))
                              orderby x.CreatedTime
                              select new CharacterDTO
                              {
                                  id=x.id, name=x.name,description=x.description,type=x.type
                              }
                              ).ToListAsync();
            return list;
        }
    }
}