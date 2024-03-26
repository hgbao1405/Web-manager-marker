using CharacterRepo.Data;
using CharacterRepo.Models;
using CharacterRepo.ModelView;
using CharacterRepo.Presentation.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharacterModule.Controllers
{
    public class GetAllBackGroundQuery : IRequest<List<BackgroundDTO>>
    {
        public string keyWord { get; set; }

        public GetAllBackGroundQuery(string keyWord)
        {
            this.keyWord = keyWord;
        }
    }
    public class GetAllBackGroundHandel : IRequestHandler<GetAllBackGroundQuery, List<BackgroundDTO>>
    {
        private readonly MyWebContext _context;

        public GetAllBackGroundHandel(MyWebContext context)
        {
            _context = context;
        }

        public async Task<List<BackgroundDTO>> Handle(GetAllBackGroundQuery request, CancellationToken cancellationToken)
        {
            var list = await (from x in _context.Background.Where(x => x.IsDeleted != true)
                              where (string.IsNullOrEmpty(request.keyWord) || x.Title.Contains(request.keyWord) || x.Description.Contains(request.keyWord))
                              orderby x.CreatedTime
                              select new BackgroundDTO
                              {
                                  id = x.id,
                                  Title =x.Title,
                                  Description=x.Description,
                                  Marker=x.Marker,
                              }
                              ).ToListAsync();
            return list;
        }
    }
}