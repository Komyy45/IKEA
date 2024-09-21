using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Common.Enums;

namespace Linkdev.IKEA.BLL.Models.Employees
{
	public class EmployeeDto
	{
		public string Name { get; set; } = null!;

		public int? Age { get; set; }

		public decimal Salary { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }

		public string? Email { get; set; } = null!;

		public string Gender { get; set; } = null!;

		[Display(Name = "Employee Type")]
		public string EmployeeType { get; set; } = null!;
	}
}
