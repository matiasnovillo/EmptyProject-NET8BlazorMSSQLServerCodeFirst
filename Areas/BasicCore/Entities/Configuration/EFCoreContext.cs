using Microsoft.EntityFrameworkCore;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration;
using EmptyProject.Areas.BasicCore.Entities.EntitiesConfiguration;

namespace EmptyProject.Areas.BasicCore.Entities.Configuration
{
    public class EFCoreContext : DbContext
    {
        protected IConfiguration _configuration { get; }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }
        public DbSet<Failure> Failure { get; set; }
        public DbSet<Parameter> Parameter { get; set; }

        public EFCoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder
                    .UseSqlServer(_configuration.GetConnectionString("EmptyProject"));
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
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
                modelBuilder.ApplyConfiguration(new MenuConfiguration());
                modelBuilder.ApplyConfiguration(new RoleMenuConfiguration());
                modelBuilder.ApplyConfiguration(new FailureConfiguration());
                modelBuilder.ApplyConfiguration(new ParameterConfiguration());

                modelBuilder.Entity<User>().HasData(new User
                {
                    UserId = 1,
                    Email = "novillo.matias1@gmail.com",
                    Password = "Pq5FM4q7dDtlZBGcn0w8P0XjnEPDlTCcLUY5/bWVcuVJ4/kXRyHp62hPgry0R/ur+kEspHc+HK6XqqvA8OLXLw==",
                    RoleId = 1,
                });

                modelBuilder.Entity<Role>().HasData(new Role
                {
                    RoleId = 1,
                    Name = "Administrator"
                });

                modelBuilder.Entity<Role>().HasData(new Role
                {
                    RoleId = 2,
                    Name = "Client"
                });

                #region Menu
                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 1,
                    Name = "BasicCore",
                    MenuFatherId = 0,
                    Order = 100,
                    URLPath = "",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 2,
                    Name = "Failure",
                    MenuFatherId = 1,
                    Order = 0,
                    URLPath = "/BasicCore/FailurePage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 3,
                    Name = "Parameter",
                    MenuFatherId = 1,
                    Order = 0,
                    URLPath = "/BasicCore/ParameterPage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 4,
                    Name = "BasicCulture",
                    MenuFatherId = 0,
                    Order = 200,
                    URLPath = "",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 5,
                    Name = "City",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/CityPage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 6,
                    Name = "State",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/StatePage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 7,
                    Name = "Country",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/CountryPage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 8,
                    Name = "Planet",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/PlanetPage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 9,
                    Name = "Sex",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/SexPage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 10,
                    Name = "CMSCore",
                    MenuFatherId = 0,
                    Order = 300,
                    URLPath = "",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 11,
                    Name = "User",
                    MenuFatherId = 10,
                    Order = 0,
                    URLPath = "/CMSCore/UserPage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 12,
                    Name = "Role",
                    MenuFatherId = 10,
                    Order = 0,
                    URLPath = "/CMSCore/RolePage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 13,
                    Name = "Menu",
                    MenuFatherId = 10,
                    Order = 0,
                    URLPath = "/CMSCore/MenuPage",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<Menu>().HasData(new Menu
                {
                    MenuId = 14,
                    Name = "Permission",
                    MenuFatherId = 10,
                    Order = 0,
                    URLPath = "/CMSCore/Permission",
                    IconURLPath = "",
                    Active = true
                });
                #endregion

                #region RoleMenu (Permission)
                modelBuilder.Entity<RoleMenu>().HasData(new RoleMenu
                {
                    RoleMenuId = 1,
                    RoleId = 1,
                    MenuId = 10
                });

                modelBuilder.Entity<RoleMenu>().HasData(new RoleMenu
                {
                    RoleMenuId = 2,
                    RoleId = 1,
                    MenuId = 14
                });
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
