using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Common.Enums;
using Linkdev.IKEA.DAL.Entities.Departments;

namespace Linkdev.IKEA.DAL.Entities.Employees
{
	public class Employee : ModelBase
	{
        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string Address { get; set; } = null!;

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }

        public string? ImageUrl { get; set; }
    }
}
