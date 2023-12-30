using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entity)
        {
            try
            {
                //UserId (clave principal autoincremental)
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .HasMaxLength(380);

                entity.Property(e => e.Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
