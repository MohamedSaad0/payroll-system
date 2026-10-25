using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.IncentivePercentage)
                .HasColumnType("decimal(5,2)");

            builder.HasIndex(d => d.Name)
                .IsUnique()
                .HasDatabaseName("IX_Departments_Name");
        }


    }
}
