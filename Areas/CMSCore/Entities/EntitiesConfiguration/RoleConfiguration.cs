using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> entity)
        {
            try
            {
                //RoleId
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd();

                //Name
                entity.Property(e => e.Name)
                    .IsRequired(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
