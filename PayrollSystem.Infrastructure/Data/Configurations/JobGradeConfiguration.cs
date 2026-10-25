using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Data.Configurations
{
    public class JobGradeConfiguration : IEntityTypeConfiguration<JobGrade>
    {
        public void Configure(EntityTypeBuilder<JobGrade> builder)
        {
            builder.ToTable("JobGrades");

            builder.HasKey(jg => jg.Id);

            builder.Property(jg => jg.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(jg => jg.BaseSalary)
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(jg => jg.Name)
                .IsUnique()
                .HasDatabaseName("IX_JobGrades_Name");
        }
    }
}
