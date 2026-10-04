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
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Actor_in_film> ActorsInFilm { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Rewards_in_film> RewardsInFilm { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Comments_in_film> CommentsInFilm { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rates_Film> RatesFilm { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Login>().ToTable("Login");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Adult_rating>().ToTable("Adult_rating");
            modelBuilder.Entity<Seans>().ToTable("Seans");

            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<Actor_in_film>().ToTable("Actors_in_film");

            modelBuilder.Entity<Reward>().ToTable("Reward");
            modelBuilder.Entity<Rewards_in_film>().ToTable("Rewards_in_film");

            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Comments_in_film>().ToTable("Comments_in_film");
            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<Rates_Film>().ToTable("Users_rates");


        }
    }
}
