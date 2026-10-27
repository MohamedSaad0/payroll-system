using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollSystem.Application.DTOs.Attendance;
using PayrollSystem.Application.Interfaces.Services;

namespace PayrollSystem.Web.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IEmployeeService _employeeService;

        public AttendanceController(
            IAttendanceService attendanceService,
            IEmployeeService employeeService)
        {
            _attendanceService = attendanceService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(int? year, int? month, CancellationToken cancellationToken)
        {
            var targetYear = year ?? DateTime.Today.Year;
            var targetMonth = month ?? DateTime.Today.Month;

            var policies = await _attendanceService.GetAllPoliciesAsync(cancellationToken);
            var absences = await _attendanceService.GetAbsencesByMonthAsync(targetYear, targetMonth, cancellationToken);

            ViewBag.Policies = policies ;
            ViewBag.SelectedYear = targetYear;
            ViewBag.SelectedMonth = targetMonth;

            return View(absences);
        }

        public async Task<IActionResult> EditPolicy(int id, CancellationToken cancellationToken)
        {
            var dto = await _attendanceService.GetPolicyForEditAsync(id, cancellationToken);
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPolicy(int id, AbsencePolicyFormDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            ModelState.Remove("Bracket");

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _attendanceService.UpdatePolicyAsync(dto, cancellationToken);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RecordAbsence(CancellationToken cancellationToken)
        {
            await PopulateEmployeesDropdownAsync(cancellationToken);
            return View(new EmployeeAbsenceFormDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecordAbsence(EmployeeAbsenceFormDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await PopulateEmployeesDropdownAsync(cancellationToken);
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _attendanceService.CreateAbsenceAsync(dto, cancellationToken);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                await PopulateEmployeesDropdownAsync(cancellationToken);
                return View(dto);
            }

            return RedirectToAction(nameof(Index), new { year = dto.Year, month = dto.Month });
        }

        public async Task<IActionResult> EditAbsence(int id, CancellationToken cancellationToken)
        {
            var dto = await _attendanceService.GetAbsenceForEditAsync(id, cancellationToken);
            if (dto == null)
            {
                return NotFound();
            }

            await PopulateEmployeesDropdownAsync(cancellationToken);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbsence(int id, EmployeeAbsenceFormDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                await PopulateEmployeesDropdownAsync(cancellationToken);
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _attendanceService.UpdateAbsenceAsync(dto, cancellationToken);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                await PopulateEmployeesDropdownAsync(cancellationToken);
                return View(dto);
            }

            return RedirectToAction(nameof(Index), new { year = dto.Year, month = dto.Month });
        }

        private async Task PopulateEmployeesDropdownAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);
            ViewBag.Employees = new SelectList(employees, "Id", "Name");
        }
    }
}
