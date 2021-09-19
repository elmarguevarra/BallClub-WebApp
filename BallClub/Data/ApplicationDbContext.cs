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

        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables
            modelBuilder.Entity<Season>().ToTable("Seasons");
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Player>().ToTable("Players");

            // Configure Primary Keys  
            modelBuilder.Entity<Season>().HasKey(x => x.SeasonId).HasName("PK_Seasons");
            modelBuilder.Entity<Game>().HasKey(x => x.GameId).HasName("PK_Games");
            modelBuilder.Entity<Team>().HasKey(x => x.TeamId).HasName("PK_Teams");
            modelBuilder.Entity<Player>().HasKey(x => x.PlayerId).HasName("PK_Players");

            // Configure indexes
            modelBuilder.Entity<Season>().HasIndex(x => x.Name).HasDatabaseName("Idx_SeasonName");

            modelBuilder.Entity<Game>().HasIndex(x => x.SeasonId).HasDatabaseName("Idx_GameSeason");
            modelBuilder.Entity<Game>().HasIndex(x => x.Schedule).HasDatabaseName("Idx_GameSchedule");
            modelBuilder.Entity<Game>().HasIndex(x => x.GameId).HasDatabaseName("Idx_GameTeamId");
            modelBuilder.Entity<Team>().HasIndex(x => x.Name).HasDatabaseName("Idx_TeamName");
            modelBuilder.Entity<Player>().HasIndex(x => x.FirstName).HasDatabaseName("Idx_FirstName");
            modelBuilder.Entity<Player>().HasIndex(x => x.LastName).HasDatabaseName("Idx_LastName");


            // Configure columns
            modelBuilder.Entity<Season>().Property(x => x.SeasonId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Season>().Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<Game>().Property(x => x.SeasonId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Game>().Property(x => x.GameId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Game>().Property(x => x.Schedule).HasColumnType("DateTime").IsRequired();

            modelBuilder.Entity<Game>()
                .Property(e => e.TeamIds)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Game>()
                .Property(e => e.PlayerIds)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Team>().Property(x => x.TeamId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Team>().Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<Player>().Property(x => x.PlayerId).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Player>().Property(x => x.Username).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Player>().Property(x => x.TeamId).HasColumnType("int").IsRequired();
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
            modelBuilder.Entity<Game>().HasOne<Season>().WithMany()
                .HasPrincipalKey(ug => ug.SeasonId).HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Games_Seasons");

            modelBuilder.Entity<Game>().HasMany<Team>().WithOne()
                //.HasPrincipalKey(ug => ug.TeamIds).HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Games_Teams");

            modelBuilder.Entity<Game>().HasMany<Player>().WithOne()
                //.HasPrincipalKey(ug => ug.PlayerIds).HasForeignKey(x => x)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Games_Players");

            modelBuilder.Entity<Player>().HasOne<Team>().WithMany()
                .HasPrincipalKey(t => t.TeamId).HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Players_Teams");
        }
    }
}
