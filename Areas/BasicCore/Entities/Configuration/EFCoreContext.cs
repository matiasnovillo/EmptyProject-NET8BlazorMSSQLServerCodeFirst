using Microsoft.EntityFrameworkCore;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration;

namespace EmptyProject.Areas.BasicCore.Entities.Configuration
{
    public class EFCoreContext : DbContext
    {
        protected IConfiguration _configuration { get; }

        public DbSet<UserEntity> DbSetUser { get; set; }
        public DbSet<RoleEntity> DbSetRole { get; set; }
        public DbSet<MenuEntity> DbSetMenu { get; set; }
        public DbSet<RoleMenuEntity> DbSetRoleMenu { get; set; }

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
                modelBuilder.ApplyConfiguration(new UserConfiguration());
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
                modelBuilder.ApplyConfiguration(new MenuConfiguration());
                modelBuilder.ApplyConfiguration(new RoleMenuConfiguration());

                modelBuilder.Entity<UserEntity>().HasData(new UserEntity
                {
                    UserId = 1,
                    Email = "novillo.matias1@gmail.com",
                    Password = "Pq5FM4q7dDtlZBGcn0w8P0XjnEPDlTCcLUY5/bWVcuVJ4/kXRyHp62hPgry0R/ur+kEspHc+HK6XqqvA8OLXLw=="
                });

                modelBuilder.Entity<RoleEntity>().HasData(new RoleEntity
                {
                    RoleId = 1,
                    Name = "Administrator"
                });

                #region Menu
                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 1,
                    Name = "BasicCore",
                    MenuFatherId = 0,
                    Order = 100,
                    URLPath = "",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 2,
                    Name = "Failure",
                    MenuFatherId = 1,
                    Order = 0,
                    URLPath = "/BasicCore/Failure",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 3,
                    Name = "Parameter",
                    MenuFatherId = 1,
                    Order = 0,
                    URLPath = "/BasicCore/Parameter",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 4,
                    Name = "BasicCulture",
                    MenuFatherId = 0,
                    Order = 200,
                    URLPath = "",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 5,
                    Name = "City",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/City",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 6,
                    Name = "State",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/State",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 7,
                    Name = "Country",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/Country",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 8,
                    Name = "Planet",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/Planet",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 9,
                    Name = "Sex",
                    MenuFatherId = 4,
                    Order = 0,
                    URLPath = "/BasicCulture/Sex",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 10,
                    Name = "CMSCore",
                    MenuFatherId = 0,
                    Order = 300,
                    URLPath = "",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 11,
                    Name = "User",
                    MenuFatherId = 10,
                    Order = 0,
                    URLPath = "/CMSCore/User",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 12,
                    Name = "Role",
                    MenuFatherId = 10,
                    Order = 0,
                    URLPath = "/CMSCore/Role",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
                {
                    MenuId = 13,
                    Name = "Menu",
                    MenuFatherId = 10,
                    Order = 0,
                    URLPath = "/CMSCore/Menu",
                    IconURLPath = "",
                    Active = true
                });

                modelBuilder.Entity<MenuEntity>().HasData(new MenuEntity
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
                modelBuilder.Entity<RoleMenuEntity>().HasData(new RoleMenuEntity
                {
                    RoleMenuId = 1,
                    RoleId = 1,
                    MenuId = 10
                });

                #region RoleMenu (Permission)
                modelBuilder.Entity<RoleMenuEntity>().HasData(new RoleMenuEntity
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
