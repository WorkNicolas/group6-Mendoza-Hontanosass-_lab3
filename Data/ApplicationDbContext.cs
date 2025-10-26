
/// <summary>
/// Application Database Context
/// </summary>
/// <remarks>
/// Entity Framework Core database context for the podcast application
/// Manages Podcasts, Episodes, Users, and Subscriptions in SQL Server
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>1.0</version>
/// <date>2025-10-24</date>
using group6_Mendoza_Hontanosass__lab3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace group6_Mendoza_Hontanosass__lab3.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Podcast-User Relationship
            builder.Entity<Podcast>()
                .HasOne(p => p.Creator)
                .WithMany(u => u.Podcasts)
                .HasForeignKey(p => p.CreatorID)
                .OnDelete(DeleteBehavior.Restrict);

            // Episode-Podcast relationship
            builder.Entity<Episode>()
                .HasOne(e => e.Podcast)
                .WithMany(p => p.Episodes)
                .HasForeignKey(e => e.PodcastID)
                .OnDelete(DeleteBehavior.Cascade);

            // Subscription relationships
            builder.Entity<Subscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Subscription>()
                .HasOne(s => s.Podcast)
                .WithMany(p => p.Subscriptions)
                .HasForeignKey(s => s.PodcastID)
                .OnDelete(DeleteBehavior.Cascade);

            // Create unique index on Subscription (UserID, PodcastID)
            builder.Entity<Subscription>()
                .HasIndex(s => new { s.UserID, s.PodcastID })
                .IsUnique();
        }
    }
}