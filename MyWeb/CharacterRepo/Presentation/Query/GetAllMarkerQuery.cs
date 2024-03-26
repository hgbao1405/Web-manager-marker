using CharacterRepo.Data;
using CharacterRepo.Models;
using CharacterRepo.ModelView;
using CharacterRepo.Presentation.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CharacterRepo.Presentation.Query
{
    public class GetAllMarkerQuery : IRequest<List<MarkersModelView>>
    {
        public string keyWord { get; set; }

        public GetAllMarkerQuery(string keyWord)
        {
            this.keyWord = keyWord;
        }
    }
    public class GetAllMarkerHandle : IRequestHandler<GetAllMarkerQuery, List<MarkersModelView>>
    {
        private readonly MyWebContext _context;

        public GetAllMarkerHandle(MyWebContext context)
        {
            _context = context;
        }

        public async Task<List<MarkersModelView>> Handle(GetAllMarkerQuery request, CancellationToken cancellationToken)
        {
            var list = await (from m in _context.Markers.Where(x => x.IsDeleted != true)
                              .Include(x => x.type).Include(x => x.Images).Include(x => x.Characters).Include(x => x.Backgrounds)
                              orderby m.CreatedTime
                              select new MarkersModelView
                              {
                                  x=m.x,
                                  y=m.y,
                                  z=m.z,
                                  type=m.type,
                                  id=m.id,
                                  character=m.type.Name=="Character"?m.Characters:null,
                                  background= m.type.Name == "Background" ? m.Backgrounds:null,
                              }
                              ).ToListAsync();
            return list;
        }
    }

    public class MarkersModelView
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public TypeMaker type { get; set; }
        public int id { get; set; }
        public Character character { get; set; }
        public Background background { get; set; }
    }
}