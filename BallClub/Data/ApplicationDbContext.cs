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

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables  
            modelBuilder.Entity<Player>().ToTable("Players");

            // Configure Primary Keys  
            modelBuilder.Entity<Player>().HasKey(u => u.Id).HasName("PK_Players");

            // Configure indexes  
            modelBuilder.Entity<Player>().HasIndex(u => u.FirstName).HasDatabaseName("Idx_FirstName");
            modelBuilder.Entity<Player>().HasIndex(u => u.LastName).HasDatabaseName("Idx_LastName");

            // Configure columns  

            modelBuilder.Entity<Player>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Player>().Property(u => u.Username).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Player>().Property(u => u.TeamId).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Player>().Property(u => u.SeasonId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Player>().Property(u => u.FirstName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Player>().Property(u => u.MiddleName).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<Player>().Property(u => u.LastName).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<Player>().Property(u => u.Suffix).HasColumnType("nvarchar(50)").IsRequired(false);
            modelBuilder.Entity<Player>().Property(u => u.Points).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(u => u.Assists).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(u => u.Rebounds).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(u => u.Steals).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(u => u.Blocks).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(u => u.Wins).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Player>().Property(u => u.Loss).HasColumnType("int").IsRequired(true);

            modelBuilder.Entity<Player>()
                .Property(e => e.SocialMediaLinks)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            // Configure relationships  
            //modelBuilder.Entity<User>().HasOne<UserGroup>().WithMany()
            //    .HasPrincipalKey(ug => ug.Id).HasForeignKey(u => u.UserGroupId)
            //    .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Users_UserGroups");
        }
    }
}
