using System.ComponentModel.DataAnnotations;
using Linkdev.IKEA.DAL.Entities.Common.Enums;

namespace Linkdev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        [MaxLength(50, ErrorMessage = "Name must be less than 50 Characters length")]
        [MinLength(2, ErrorMessage = "Name must be More than than 2 Characters length")]
        public string Name { get; set; } = null!;

        [Range(22, 60)]
        public int? Age { get; set; }

        [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "The Address Must be Like '123-Street-City-Country'")]
        public string Address { get; set; } = null!;

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; } = null!;

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public string Gender { get; set; } = null!;

        [Display(Name = "Employee Type")]
        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }
    }
}
