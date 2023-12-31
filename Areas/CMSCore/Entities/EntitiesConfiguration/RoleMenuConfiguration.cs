using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration
{
    public class RoleMenuConfiguration : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> entity)
        {
            try
            {
                //RoleMenuId
                entity.HasKey(e => e.RoleMenuId);
                entity.Property(e => e.RoleMenuId)
                    .ValueGeneratedOnAdd();

                //MenuId
                entity.Property(e => e.MenuId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //RoleId
                entity.Property(e => e.RoleId)
                    .HasColumnType("int")
                    .IsRequired(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
