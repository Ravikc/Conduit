using Conduit.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.Data
{
    public class ConduitDbContext : IdentityDbContext<ApplicationUser>
    {
        public ConduitDbContext(DbContextOptions<ConduitDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>()
                .HasIndex(a => a.Slug)
                .IsUnique();
        }
    }
}