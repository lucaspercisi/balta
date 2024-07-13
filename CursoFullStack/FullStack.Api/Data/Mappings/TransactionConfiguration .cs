using FullStack.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullStack.Api.Data.Mappings
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Title)
                   .IsRequired(true)
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(80);

            builder.Property(x => x.Type)
                   .IsRequired(true)
                   .HasColumnType("SMALLINT");

            builder.Property(x => x.Amount)
                   .IsRequired(true)
                   .HasColumnType("MONEY");

            builder.Property(x => x.CreatedAt)
                   .IsRequired(true);

            builder.Property(x => x.PaidOrReceivedAt)
                   .IsRequired(false);

            builder.Property(x => x.UserId)
                   .IsRequired(true)
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(100);
        }
    }
}
