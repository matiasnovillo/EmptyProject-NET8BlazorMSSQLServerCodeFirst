using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            try
            {
                //MenuId
                entity.HasKey(e => e.MenuId);
                entity.Property(e => e.MenuId)
                    .ValueGeneratedOnAdd();

                //Name
                entity.Property(e => e.Name)
                    .IsRequired(false);

                //MenuFatherId
                entity.Property(e => e.MenuFatherId)
                    .HasColumnType("int");

                //Order
                entity.Property(e => e.Order)
                    .HasColumnType("int");

                //URLPath
                entity.Property(e => e.URLPath)
                    .IsRequired(false);

                //IconURLPath
                entity.Property(e => e.IconURLPath)
                    .IsRequired(false);

                //Active
                entity.Property(e => e.Active)
                    .HasColumnType("tinyint");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
