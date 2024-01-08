using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace EmptyProject.Areas.BasicCore.Entities.EntitiesConfiguration
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> entity)
        {
            try
            {
                //ParameterId
                entity.HasKey(e => e.ParameterId);
                entity.Property(e => e.ParameterId)
                    .ValueGeneratedOnAdd();

                //Active
                entity.Property(e => e.Active)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                //UserCreationId
                entity.Property(e => e.UserCreationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //UserLastModificationId
                entity.Property(e => e.UserLastModificationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //Name
                entity.Property(e => e.Name)
                    .HasColumnType("varchar(200)")
                    .IsRequired(false);

                //Value
                entity.Property(e => e.Value)
                    .HasColumnType("text")
                    .IsRequired(false);

                //IsPrivate
                entity.Property(e => e.IsPrivate)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
