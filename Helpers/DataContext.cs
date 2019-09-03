using Microsoft.EntityFrameworkCore;
using JWTAuth.Models;

namespace JWTAuth.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region UserSeed
            modelBuilder.Entity<User>().HasData(new User { 
                UserName = "pleavitt", 
                PasswordHash = "http://sample.com" 
            });
            #endregion

        }
    }
}