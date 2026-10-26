using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollSystem.Application.DTOs.Employees
{
    public class EmployeeFormDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الموظف مطلوب.")]
        [MaxLength(200, ErrorMessage = "الاسم لا يمكن أن يتجاوز 200 حرف.")]
        [Display(Name = "الاسم الرباعي")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب.")]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الميلاد")]
        public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-25);

        [Required(ErrorMessage = "العنوان مطلوب.")]
        [MaxLength(500, ErrorMessage = "العنوان لا يمكن أن يتجاوز 500 حرف.")]
        [Display(Name = "العنوان")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب.")]
        [RegularExpression(@"^01[0-2,5]\d{8}$", ErrorMessage = "رقم الهاتف غير صحيح (يجب أن يكون رقم محمول مصري صحيح).")]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة.")]
        [MaxLength(150, ErrorMessage = "البريد الإلكتروني لا يمكن أن يتجاوز 150 حرف.")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ التعيين مطلوب.")]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ التعيين")]
        public DateTime HireDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "يجب اختيار الدرجة الوظيفية.")]
        [Display(Name = "الدرجة الوظيفية")]
        public int JobGradeId { get; set; }

        [Required(ErrorMessage = "يجب اختيار القسم.")]
        [Display(Name = "القسم التابع له")]
        public int DepartmentId { get; set; }
    }

}