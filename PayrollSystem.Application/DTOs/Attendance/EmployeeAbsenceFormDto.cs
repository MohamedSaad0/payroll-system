using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollSystem.Application.DTOs.Attendance
{
    public class EmployeeAbsenceFormDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "يجب اختيار الموظف.")]
        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "السنة مطلوبة.")]
        [Display(Name = "السنة")]
        public int Year { get; set; } = DateTime.Today.Year;

        [Required(ErrorMessage = "الشهر مطلوب.")]
        [Range(1, 12, ErrorMessage = "الشهر يجب أن يكون بين 1 و 12.")]
        [Display(Name = "الشهر")]
        public int Month { get; set; } = DateTime.Today.Month;

        [Required(ErrorMessage = "عدد أيام الغياب مطلوب.")]
        [Range(0, 31, ErrorMessage = "أيام الغياب يجب أن تكون بين 0 و 31.")]
        [Display(Name = "أيام الغياب")]
        public int AbsenceDaysCount { get; set; }
    }
}
