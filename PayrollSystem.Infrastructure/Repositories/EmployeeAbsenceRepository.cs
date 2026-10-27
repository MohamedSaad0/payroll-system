using Microsoft.EntityFrameworkCore;
using PayrollSystem.Application.Interfaces.Repositories;
using PayrollSystem.Domain.Entities;
using PayrollSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Infrastructure.Repositories
{
    public class EmployeeAbsenceRepository : IEmployeeAbsenceRepository
    {
        private readonly PayrollDbContext _context;

        public EmployeeAbsenceRepository(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<EmployeeAbsence>> GetByMonthAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            return await _context.EmployeeAbsences.Include(a => a.Employee).ThenInclude(e => e.Department)
                .Where(a => a.Year == year && a.Month == month).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<EmployeeAbsence?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.EmployeeAbsences.Include(a => a.Employee).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<EmployeeAbsence?> GetByEmployeeAndMonthAsync(int employeeId, int year, int month, CancellationToken cancellationToken = default)
        {
            return await _context.EmployeeAbsences
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Year == year && a.Month == month, cancellationToken);
        }

        public async Task AddAsync(EmployeeAbsence absence, CancellationToken cancellationToken = default)
        {
            _context.EmployeeAbsences.Add(absence);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(EmployeeAbsence absence, CancellationToken cancellationToken = default)
        {
            _context.EmployeeAbsences.Update(absence);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task RemoveAsync(EmployeeAbsence absence, CancellationToken cancellationToken = default)
        {
            _context.EmployeeAbsences.Remove(absence);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
