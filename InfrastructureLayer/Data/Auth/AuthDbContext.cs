using EVO.DomainLayer.Entity.Configurations.Auth;
using EVO.DomainLayer.Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace EVO.InfrastructureLayer.Data.Auth
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken_> RefreshToken { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}
