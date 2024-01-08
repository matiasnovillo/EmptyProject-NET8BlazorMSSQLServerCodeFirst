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
    public class FailureConfiguration : IEntityTypeConfiguration<Failure>
    {
        public void Configure(EntityTypeBuilder<Failure> entity)
        {
            try
            {
                //FailureId
                entity.HasKey(e => e.FailureId);
                entity.Property(e => e.FailureId)
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

                //Message
                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .IsRequired(false);

                //EmergencyLevel
                entity.Property(e => e.EmergencyLevel)
                    .HasColumnType("int")
                    .IsRequired(true);

                //StackTrace
                entity.Property(e => e.StackTrace)
                    .HasColumnType("text")
                    .IsRequired(false);

                //Source
                entity.Property(e => e.Source)
                    .HasColumnType("text")
                    .IsRequired(false);

                //Comment
                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .IsRequired(false);

                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
