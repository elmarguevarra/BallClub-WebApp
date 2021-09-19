using BallClub.Repository.DTO.Messages;
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

        public DbSet<GameDTO> Games { get; set; }
        public DbSet<TeamDTO> Teams { get; set; }
        public DbSet<PlayerDTO> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables
            modelBuilder.Entity<SeasonDTO>().ToTable("Seasons");
            modelBuilder.Entity<GameDTO>().ToTable("Games");
            modelBuilder.Entity<TeamDTO>().ToTable("Teams");
            modelBuilder.Entity<PlayerDTO>().ToTable("Players");

            // Configure Primary Keys  
            modelBuilder.Entity<SeasonDTO>().HasKey(x => x.SeasonId).HasName("PK_Seasons");
            modelBuilder.Entity<GameDTO>().HasKey(x => x.GameId).HasName("PK_Games");
            modelBuilder.Entity<TeamDTO>().HasKey(x => x.TeamId).HasName("PK_Teams");
            modelBuilder.Entity<PlayerDTO>().HasKey(x => x.PlayerId).HasName("PK_Players");

            // Configure indexes
            modelBuilder.Entity<SeasonDTO>().HasIndex(x => x.Name).HasDatabaseName("Idx_SeasonName");

            modelBuilder.Entity<GameDTO>().HasIndex(x => x.SeasonId).HasDatabaseName("Idx_GameSeason");
            modelBuilder.Entity<GameDTO>().HasIndex(x => x.Schedule).HasDatabaseName("Idx_GameSchedule");
            modelBuilder.Entity<GameDTO>().HasIndex(x => x.GameId).HasDatabaseName("Idx_GameTeamId");
            modelBuilder.Entity<TeamDTO>().HasIndex(x => x.Name).HasDatabaseName("Idx_TeamName");
            modelBuilder.Entity<PlayerDTO>().HasIndex(x => x.FirstName).HasDatabaseName("Idx_FirstName");
            modelBuilder.Entity<PlayerDTO>().HasIndex(x => x.LastName).HasDatabaseName("Idx_LastName");


            // Configure columns
            modelBuilder.Entity<SeasonDTO>().Property(x => x.SeasonId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<SeasonDTO>().Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<GameDTO>().Property(x => x.SeasonId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<GameDTO>().Property(x => x.GameId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<GameDTO>().Property(x => x.Schedule).HasColumnType("DateTime").IsRequired();

            modelBuilder.Entity<GameDTO>()
                .Property(e => e.TeamIds)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<GameDTO>()
                .Property(e => e.PlayerIds)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<TeamDTO>().Property(x => x.TeamId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<TeamDTO>().Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<PlayerDTO>().Property(x => x.PlayerId).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Username).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<PlayerDTO>().Property(x => x.TeamId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<PlayerDTO>().Property(x => x.FirstName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<PlayerDTO>().Property(x => x.MiddleName).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.LastName).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Suffix).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Points).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Assists).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Rebounds).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Steals).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Blocks).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Wins).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<PlayerDTO>().Property(x => x.Loss).HasColumnType("int").IsRequired(true);

            modelBuilder.Entity<PlayerDTO>()
                .Property(e => e.SocialMediaLinks)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            //Configure relationships
            modelBuilder.Entity<GameDTO>().HasOne<SeasonDTO>().WithMany()
                .HasPrincipalKey(ug => ug.SeasonId).HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Games_Seasons");

            modelBuilder.Entity<GameDTO>().HasMany<TeamDTO>().WithOne()
                //.HasPrincipalKey(ug => ug.TeamIds).HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Games_Teams");

            modelBuilder.Entity<GameDTO>().HasMany<PlayerDTO>().WithOne()
                //.HasPrincipalKey(ug => ug.PlayerIds).HasForeignKey(x => x)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Games_Players");

            modelBuilder.Entity<PlayerDTO>().HasOne<TeamDTO>().WithMany()
                .HasPrincipalKey(t => t.TeamId).HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Players_Teams");
        }
    }
}
