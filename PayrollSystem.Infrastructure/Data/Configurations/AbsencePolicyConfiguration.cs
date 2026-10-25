using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Data.Configurations
{
    public class AbsencePolicyConfiguration : IEntityTypeConfiguration<AbsencePolicy>
    {
        public void Configure(EntityTypeBuilder<AbsencePolicy> builder)
        {
            builder.ToTable("AbsencePolicies");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Bracket)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Percentage)
            .HasColumnType("decimal(5,2)");

            builder.HasIndex(a => new { a.FromDays, a.ToDays })
            .IsUnique()
            .HasDatabaseName("IX_AbsencePolicies_FromDays_ToDays");

        }
    }
}
