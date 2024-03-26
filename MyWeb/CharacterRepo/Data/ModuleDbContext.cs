using CharacterRepo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRepo.Data
{
    public abstract class ModuleDbContext : DbContext
    {
        protected abstract string Schema { get; }
        protected ModuleDbContext(DbContextOptions options) : base(options)
        {
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        public DbSet<Background> Background { get; set; } = default!;

        public DbSet<Character> Character { get; set; } = default!;

        public DbSet<Image> Images { get; set; } = default!;

        public DbSet<Marker> Markers { get; set; } = default!;

        public DbSet<TypeMaker> TypeMakers { get; set; } = default!;
    }
}
