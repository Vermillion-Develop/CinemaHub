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
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Ticket_type> Ticket_types { get; set; }
        public DbSet<Zal> Zals { get; set; }
        public DbSet<Mesto_status> MestoStatuses { get; set; }
        public DbSet<Mesto_row> MestoRowes { get; set; }
        public DbSet<Mesto> Mestos { get; set; }
        public DbSet<Mesta_in_zal> MestosInZals { get; set; }
        public DbSet<User_tickets> UserTickets { get; set; }



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

            modelBuilder.Entity<Ticket_type>().ToTable("Ticket_type");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");

            modelBuilder.Entity<Zal>().ToTable("Zal");

            modelBuilder.Entity<Mesto_row>().ToTable("Mesto_row");
            modelBuilder.Entity<Mesto_status>().ToTable("Mesto_status");
            modelBuilder.Entity<Mesto>().ToTable("Mesto");
            modelBuilder.Entity<Mesta_in_zal>().ToTable("Mesta_in_zal");

            modelBuilder.Entity<User_tickets>().ToTable("User_tickets");

        }
    }
}
