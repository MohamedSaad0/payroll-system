using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Application.DTOs.Departments;
using PayrollSystem.Application.DTOs.Incentives;
using PayrollSystem.Application.Interfaces.Services;

namespace PayrollSystem.Web.Controllers
{
    public class IncentivesController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IIncentiveService _incentiveService;

        public IncentivesController(
            IDepartmentService departmentService,
            IIncentiveService incentiveService)
        {
            _departmentService = departmentService;
            _incentiveService = incentiveService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var departmentsTask = await _departmentService.GetAllAsync(cancellationToken);
            var seniorityRulesTask = await _incentiveService.GetAllSeniorityRulesAsync(cancellationToken);

            //await Task.WhenAll(departmentsTask, seniorityRulesTask);

            ViewBag.Departments = departmentsTask;

            return View(seniorityRulesTask);
        }
        public async Task<IActionResult> EditDepartmentIncentive(int id, CancellationToken cancellationToken)
        {
            var dto = await _departmentService.GetForEditAsync(id, cancellationToken);
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartmentIncentive(int id, DepartmentFormDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            ModelState.Remove("Name");

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _departmentService.UpdateAsync(dto, cancellationToken);

            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateSeniorityRule()
        {
            return View(new SeniorityIncentiveFormDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSeniorityRule(SeniorityIncentiveFormDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _incentiveService.CreateSeniorityRuleAsync(dto, cancellationToken);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditSeniorityRule(int id, CancellationToken cancellationToken)
        {
            var dto = await _incentiveService.GetSeniorityRuleForEditAsync(id, cancellationToken);
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSeniorityRule(int id, SeniorityIncentiveFormDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _incentiveService.UpdateSeniorityRuleAsync(dto, cancellationToken);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
