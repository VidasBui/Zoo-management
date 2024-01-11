using Microsoft.EntityFrameworkCore;
using Zoo_management.Data.Entities;

namespace Zoo_management.Data
{
    public class ZooDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<EnclosureObject> EnclosureObjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=ZooDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasKey(e => e.Id);
            modelBuilder.Entity<Enclosure>().HasKey(e => e.Id);
            modelBuilder.Entity<EnclosureObject>().HasKey(e => e.Id);
        }
    }
}
