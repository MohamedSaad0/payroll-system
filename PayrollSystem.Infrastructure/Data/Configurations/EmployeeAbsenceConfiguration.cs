using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Data.Configurations
{
    public class EmployeeAbsenceConfiguration : IEntityTypeConfiguration<EmployeeAbsence>
    {
        public void Configure(EntityTypeBuilder<EmployeeAbsence> builder)
        {
            builder.ToTable("EmployeeAbsences");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.EmployeeId, e.Year, e.Month })
                .IsUnique()
                .HasDatabaseName("IX_EmployeeAbsences_EmployeeId_Year_Month");

            builder.HasIndex(e => new { e.Year, e.Month })
                .HasDatabaseName("IX_EmployeeAbsences_Year_Month");

        }
    }
}
