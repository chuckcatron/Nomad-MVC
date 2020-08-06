using Microsoft.EntityFrameworkCore;
using Nomad_MVC.Models;

namespace Nomad_MVC.Data
{
    public class Nomad_MVCContext: DbContext
    {
        public Nomad_MVCContext(DbContextOptions<Nomad_MVCContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
