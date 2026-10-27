using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollSystem.Application.DTOs.Attendance
{
    public class AbsencePolicyFormDto
    {
        public int Id { get; set; }
        public string Bracket { get; set; } = string.Empty;

        [Required(ErrorMessage = "النسبة مطلوبة.")]
        [Range(0, 100, ErrorMessage = "النسبة يجب أن تكون بين 0 و 100.")]
        [Display(Name = "النسبة (%)")]
        public decimal Percentage { get; set; }
    }
}
