using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Data.Configurations
{
    public class SeniorityIncentiveConfiguration : IEntityTypeConfiguration<SeniorityIncentive>
    {
        public void Configure(EntityTypeBuilder<SeniorityIncentive> builder)
        {
            builder.ToTable("SeniorityIncentives");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.IncentivePercentage)
            .HasColumnType("decimal(5,2)");

            builder.HasIndex(s => s.MinYears)
                .IsUnique()
                .HasDatabaseName("IX_SeniorityRules_MinYears");
        }
    }
}
