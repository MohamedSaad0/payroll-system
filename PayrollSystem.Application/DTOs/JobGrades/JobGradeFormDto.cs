using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollSystem.Application.DTOs.JobGrades
{
    public class JobGradeFormDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "المرتب الأساسي مطلوب.")]
        [Range(0, 1000000, ErrorMessage = "المرتب الأساسي غير صالح.")]
        [Display(Name = "المرتب الأساسي")]
        public decimal BaseSalary { get; set; }
    }
}
