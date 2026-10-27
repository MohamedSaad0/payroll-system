using Microsoft.AspNetCore.Mvc;

using PayrollSystem.Application.Interfaces.Services;
namespace PayrollSystem.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> Employees(CancellationToken cancellationToken)
        {
            var reports = await _reportService.GetEmployeesReportAsync(cancellationToken);
            return View(reports);
        }

        public async Task<IActionResult> Attendance(int? year, int? month, CancellationToken cancellationToken)
        {
            var targetYear = year ?? DateTime.Today.Year;
            var targetMonth = month ?? DateTime.Today.Month;

            var reports = await _reportService.GetAttendanceReportAsync(targetYear, targetMonth, cancellationToken);

            ViewBag.SelectedYear = targetYear;
            ViewBag.SelectedMonth = targetMonth;

            return View(reports);
        }

        public async Task<IActionResult> Incentives(int? year, int? month, CancellationToken cancellationToken)
        {
            var targetYear = year ?? DateTime.Today.Year;
            var targetMonth = month ?? DateTime.Today.Month;
            var reports = await _reportService.GetPayrollReportAsync(targetYear, targetMonth, cancellationToken);

            ViewBag.SelectedYear = targetYear;
            ViewBag.SelectedMonth = targetMonth;

            return View(reports);
        }

        public async Task<IActionResult> Index(int? year, int? month, CancellationToken cancellationToken)
        {
            var targetYear = year ?? DateTime.Today.Year;
            var targetMonth = month ?? DateTime.Today.Month;

            var reports = await _reportService.GetPayrollReportAsync(targetYear, targetMonth, cancellationToken);

            ViewBag.SelectedYear = targetYear;
            ViewBag.SelectedMonth = targetMonth;

            return View(reports);
        }
    }
}
