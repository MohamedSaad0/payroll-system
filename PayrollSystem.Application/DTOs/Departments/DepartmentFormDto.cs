using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollSystem.Application.DTOs.Departments
{
    public class DepartmentFormDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم القسم مطلوب.")]
        [MaxLength(100, ErrorMessage = "اسم القسم لا يمكن أن يتجاوز 100 حرف.")]
        [Display(Name = "اسم القسم")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "نسبة الحافز يجب أن تكون بين 0 و 100.")]
        [Display(Name = "نسبة الحافز (%)")]
        public decimal IncentivePercentage { get; set; }
    }
}
