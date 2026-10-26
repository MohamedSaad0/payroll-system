using Microsoft.EntityFrameworkCore;
using PayrollSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Data
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) :base(options)
        {
            
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<JobGrade> JobGrades => Set<JobGrade>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EmployeeAbsence> EmployeeAbsences => Set<EmployeeAbsence>();
        public DbSet<SeniorityIncentive> SeniorityIncentives => Set<SeniorityIncentive>();
        public DbSet<AbsencePolicy> AbsencePolicies => Set<AbsencePolicy>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PayrollDbContext).Assembly);

            modelBuilder.Seed();
        }
    }
}
