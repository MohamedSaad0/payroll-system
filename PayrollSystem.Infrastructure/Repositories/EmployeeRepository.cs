using Microsoft.EntityFrameworkCore;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Domain.Entities;
using PayrollSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PayrollDbContext _context;

        public EmployeeRepository(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Employee>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Employees.Include(e => e.Department).Include(e => e.JobGrade).AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Employee?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees.Include(e => e.Department).Include(e => e.JobGrade)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Employees.AsQueryable();

            if (excludeId.HasValue)
            {
                query = query.Where(e => e.Id != excludeId.Value);
            }

            return await query.AnyAsync(e => e.Email == email, cancellationToken);
        }

        public async Task<bool> HasAnyByDepartmentIdAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees.AnyAsync(e => e.DepartmentId == departmentId, cancellationToken);
        }

        public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
