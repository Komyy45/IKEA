using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace Linkdev.IKEA.BLL.Models.Employees
{
	public class UpdatedEmployeeDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public int? Age { get; set; }

		public string Address { get; set; } = null!;

		public decimal Salary { get; set; }

		public bool IsActive { get; set; }

		public string? Email { get; set; } = null!;

		public string? PhoneNumber { get; set; }

		public DateOnly HiringDate { get; set; }

		public string Gender { get; set; } = null!;

		public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }
    }
}
