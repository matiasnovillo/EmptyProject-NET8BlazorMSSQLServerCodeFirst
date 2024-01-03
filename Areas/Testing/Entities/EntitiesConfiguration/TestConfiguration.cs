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

                //Boolean
                entity.Property(e => e.Boolean)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                //DateTime
                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //Decimal
                entity.Property(e => e.Decimal)
                    .HasColumnType("numeric(18, 2)")
                    .IsRequired(true);

                //ForeignKeyDropdown
                entity.Property(e => e.ForeignKeyDropdown)
                    .HasColumnType("int")
                    .IsRequired(true);

                //ForeignKeyOptions
                entity.Property(e => e.ForeignKeyOptions)
                    .HasColumnType("int")
                    .IsRequired(true);

                //Integer
                entity.Property(e => e.Integer)
                    .HasColumnType("int")
                    .IsRequired(true);

                //Basic
                entity.Property(e => e.Basic)
                    .IsRequired(false)
                    .HasMaxLength(100);

                //Email
                entity.Property(e => e.Email)
                    .IsRequired(true)
                    .HasMaxLength(380);

                //File
                entity.Property(e => e.File)
                    .IsRequired(true);

                //HexColour
                entity.Property(e => e.HexColour)
                    .IsRequired(false)
                    .HasMaxLength(6);

                //Password
                entity.Property(e => e.Password)
                    .IsRequired(true);

                //PhoneNumber
                entity.Property(e => e.PhoneNumber)
                    .IsRequired(true);

                //Tag
                entity.Property(e => e.Tag)
                    .IsRequired(false);

                //TextArea
                entity.Property(e => e.TextArea)
                    .HasColumnType("text")
                    .IsRequired(true)
                    .HasMaxLength(2000);

                //TextEditor
                entity.Property(e => e.TextEditor)
                    .HasColumnType("text")
                    .IsRequired(false)
                    .HasMaxLength(2000);

                //URL
                entity.Property(e => e.URL)
                    .IsRequired(true);

                //Time
                entity.Property(e => e.Time)
                    .HasColumnType("time(7)")
                    .IsRequired(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
