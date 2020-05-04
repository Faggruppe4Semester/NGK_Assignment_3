using Microsoft.EntityFrameworkCore;
using NGK_Assignment_3.Areas.Database.Models;

namespace NGK_Assignment_3.Areas.Database
{
    public class NGKDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }

        public NGKDbContext(DbContextOptions<NGKDbContext> options)
            : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Place>().HasKey(place => new {place.Lat, place.Lon});
            

            modelBuilder.Entity<Measurement>()
                .HasOne(m => m.Place)
                .WithMany(p => p.Measurements)
                .HasForeignKey(m => new {m.PlaceLat, m.PlaceLon});

        }
    }
}