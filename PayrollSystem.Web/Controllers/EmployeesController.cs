using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollSystem.Application.DTOs.Employees;
using PayrollSystem.Application.Interfaces.Services;

namespace PayrollSystem.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IJobGradeService _jobGradeService;

        public EmployeesController(
            IEmployeeService employeeService,
            IDepartmentService departmentService,
            IJobGradeService jobGradeService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _jobGradeService = jobGradeService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);
            return View(employees);
        }
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            await PopulateDropdownsAsync(cancellationToken);
            return View(new EmployeeFormDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(cancellationToken);
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _employeeService.CreateAsync(dto, cancellationToken);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                await PopulateDropdownsAsync(cancellationToken);
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var dto = await _employeeService.GetForEditAsync(id, cancellationToken);
            if (dto == null)
            {
                return NotFound();
            }

            await PopulateDropdownsAsync(cancellationToken);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeFormDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(cancellationToken);
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _employeeService.UpdateAsync(dto, cancellationToken);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                await PopulateDropdownsAsync(cancellationToken);
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var (isSuccess, errorMessage) = await _employeeService.DeleteAsync(id, cancellationToken);
            if (!isSuccess)
            {
                return BadRequest(errorMessage);
            }

            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateDropdownsAsync(CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllAsync(cancellationToken);
            var jobGrades = await _jobGradeService.GetAllAsync(cancellationToken);

            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            ViewBag.JobGrades = new SelectList(jobGrades, "Id", "Name");
        }
    }
}
