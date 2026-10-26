using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IJobGradeRepository, JobGradeRepository>();
            services.AddScoped<IAbsencePolicyRepository, AbsencePolicyRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ISeniorityIncentiveRepository, SeniorityIncentiveRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeAbsenceRepository, EmployeeAbsenceRepository>();

            return services;
        }
    }
}
