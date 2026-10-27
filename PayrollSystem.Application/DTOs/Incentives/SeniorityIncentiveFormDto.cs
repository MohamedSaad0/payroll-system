using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollSystem.Application.DTOs.Incentives
{
    public class SeniorityIncentiveFormDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عدد السنوات مطلوب.")]
        [Range(1, 40, ErrorMessage = "عدد السنوات يجب أن يكون بين 1 و 40.")]
        [Display(Name = "أكثر من (سنوات)")]
        public int MinYears { get; set; }

        [Required(ErrorMessage = "نسبة الحافز مطلوبة.")]
        [Range(0, 100, ErrorMessage = "نسبة الحافز يجب أن تكون بين 0 و 100.")]
        [Display(Name = "نسبة الحافز (%)")]
        public decimal IncentivePercentage { get; set; }
    }
}
