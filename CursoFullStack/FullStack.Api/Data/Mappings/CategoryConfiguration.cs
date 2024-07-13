using FullStack.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullStack.Api.Data.Mappings
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Title)
                   .IsRequired(true)
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(80);
            
            builder.Property(x => x.Description)
                   .IsRequired(false)
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(255);
            
            builder.Property(x => x.UserId)
                   .IsRequired(true)
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(160);
        }
    }
}
