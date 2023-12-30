using Microsoft.EntityFrameworkCore;
using EmptyProject.Entities.EntitiesConfiguration;

namespace EmptyProject.Entities.Configuration
{
    public class EFCoreContext : DbContext
    {
        protected IConfiguration _configuration { get; }
        public EFCoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EmptyProject"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new UserConfiguration());

                modelBuilder.Entity<User>().HasData(new User
                {
                    UserId = 1,
                    Email = "novillo.matias1@gmail.com",
                    Password = "Pq5FM4q7dDtlZBGcn0w8P0XjnEPDlTCcLUY5/bWVcuVJ4/kXRyHp62hPgry0R/ur+kEspHc+HK6XqqvA8OLXLw=="
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
