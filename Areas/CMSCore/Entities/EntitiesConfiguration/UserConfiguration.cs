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
                //UserId
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd();

                //Email
                entity.Property(e => e.Email)
                    .IsRequired(false)
                    .HasMaxLength(380);

                //Password
                entity.Property(e => e.Password)
                    .IsRequired(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
