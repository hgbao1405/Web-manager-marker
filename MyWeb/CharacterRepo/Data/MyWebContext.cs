using Microsoft.EntityFrameworkCore;

namespace CharacterRepo.Data
{
    public class MyWebContext : ModuleDbContext, IMyWebContext
    {
        public MyWebContext(DbContextOptions<MyWebContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override string Schema => "Character";
    }

}