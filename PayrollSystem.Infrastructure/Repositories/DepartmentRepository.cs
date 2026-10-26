using Microsoft.EntityFrameworkCore;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Domain.Entities;
using PayrollSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly PayrollDbContext _context;

        public DepartmentRepository(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<bool> ExistsByNameAsync(string name, int? excludedId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Departments.AsQueryable();

            if (excludedId.HasValue)
            {
                query = query.Where(d => d.Id != excludedId);
            }

            return await query.AnyAsync(d => d.Name == name, cancellationToken);
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Departments.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }

        public async Task RemoveAsync(Department department, CancellationToken cancellationToken = default)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Department department, CancellationToken cancellationToken = default)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
