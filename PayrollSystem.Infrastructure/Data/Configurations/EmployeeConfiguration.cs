using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Address)
                .HasMaxLength(500);
            builder.HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("IX_Employees_Email");

            builder.HasIndex(e => e.Name)
                .HasDatabaseName("IX_Employees_Name");

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Absences)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
