using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Application.Interfaces.Services;
using PayrollSystem.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}