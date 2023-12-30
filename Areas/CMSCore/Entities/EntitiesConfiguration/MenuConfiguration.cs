using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<MenuEntity>
    {
        public void Configure(EntityTypeBuilder<MenuEntity> entity)
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
                    .HasColumnType("int")
                    .IsRequired(false);

                //Order
                entity.Property(e => e.Order)
                    .HasColumnType("int")
                    .IsRequired(false);

                //URLPath
                entity.Property(e => e.URLPath)
                    .IsRequired(false);

                //IconURLPath
                entity.Property(e => e.IconURLPath)
                    .IsRequired(false);

                //Active
                entity.Property(e => e.IconURLPath)
                    .HasColumnType("tinyint")
                    .IsRequired(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
