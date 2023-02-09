using AccountManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Persistence.EntityConfigs
{
    public class BlockWithdrawTransactionConfigs : IEntityTypeConfiguration<BlockWithdrawTransaction>
    {
        public void Configure(EntityTypeBuilder<BlockWithdrawTransaction> builder)
        {
            builder.HasKey(k => k.Id);
          
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.TraceNumber).IsRequired(false);
            builder.Property(p => p.TraceNumber).HasMaxLength(200);
            builder.Property(p => p.Date).IsRequired(false);
            builder.Property(p => p.Date).HasMaxLength(25);

            builder.HasOne(t => t.Account)
                   .WithOne(t => t.BlockWithdrawTransaction);

        }
    }
}
