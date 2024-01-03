using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.Areas.Testing.Entities.EntitiesConfiguration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> entity)
        {
            try
            {
                //TestId
                entity.HasKey(e => e.TestId);
                entity.Property(e => e.TestId)
                    .ValueGeneratedOnAdd();

                ////Email
                //entity.Property(e => e.Email)
                //    .IsRequired(false)
                //    .HasMaxLength(380);

                ////Password
                //entity.Property(e => e.Password)
                //    .IsRequired(false);

                ////RoleId
                //entity.Property(e => e.RoleId)
                //    .HasColumnType("int")
                //    .IsRequired(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
