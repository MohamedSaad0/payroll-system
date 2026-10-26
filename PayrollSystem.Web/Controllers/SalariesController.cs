using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Application.DTOs.JobGrades;
using PayrollSystem.Application.Interfaces.Services;

namespace PayrollSystem.Web.Controllers
{
    public class SalariesController : Controller
    {
        private readonly IJobGradeService _jobGradeService;

        public SalariesController(IJobGradeService jobGradeService)
        {
            _jobGradeService = jobGradeService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var jobGrades = await _jobGradeService.GetAllAsync(cancellationToken);
            return View(jobGrades);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var dto = await _jobGradeService.GetForEditAsync(id, cancellationToken);

            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobGradeFormDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var (isSuccess, errorMessage) = await _jobGradeService.UpdateAsync(dto, cancellationToken);

            if(!isSuccess)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
