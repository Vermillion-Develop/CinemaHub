using Microsoft.EntityFrameworkCore;
using CinemaHubShared.Models;

namespace CinemaHubBackground.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Adult_rating> AdultRatings { get; set; }
        public DbSet<Seans> Seanses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Login>().ToTable("Login");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Adult_rating>().ToTable("Adult_rating");
            modelBuilder.Entity<Seans>().ToTable("Seans");


        }
    }
}
