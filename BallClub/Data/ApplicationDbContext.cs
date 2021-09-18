using BallClub.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BallClub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public string ConnectionString { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables
            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Player>().ToTable("Players");

            // Configure Primary Keys  
            modelBuilder.Entity<Team>().HasKey(x => x.Id).HasName("PK_Teams");
            modelBuilder.Entity<Player>().HasKey(x => x.Id).HasName("PK_Players");

            // Configure indexes  
            modelBuilder.Entity<Team>().HasIndex(x => x.Name).HasDatabaseName("Idx_TeamName");
            modelBuilder.Entity<Player>().HasIndex(x => x.FirstName).HasDatabaseName("Idx_FirstName");
            modelBuilder.Entity<Player>().HasIndex(x => x.LastName).HasDatabaseName("Idx_LastName");


            // Configure columns  
            modelBuilder.Entity<Team>().Property(x => x.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Team>().Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<Player>().Property(x => x.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Player>().Property(x => x.Username).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Player>().Property(x => x.TeamId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Player>().Property(x => x.SeasonId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Player>().Property(x => x.FirstName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Player>().Property(x => x.MiddleName).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<Player>().Property(x => x.LastName).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<Player>().Property(x => x.Suffix).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<Player>().Property(x => x.Points).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(x => x.Assists).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(x => x.Rebounds).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(x => x.Steals).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(x => x.Blocks).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(x => x.Wins).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(x => x.Loss).HasColumnType("int").IsRequired(true);

            modelBuilder.Entity<Player>()
                .Property(e => e.SocialMediaLinks)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            //Configure relationships
            modelBuilder.Entity<Player>().HasOne<Team>().WithMany()
                .HasPrincipalKey(ug => ug.Id).HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Players_Teams");
        }
    }
}
