using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.BLL.Models.Employees;
using Linkdev.IKEA.DAL.Entities.Common.Enums;
using Linkdev.IKEA.DAL.Entities.Employees;
using Linkdev.IKEA.DAL.Presistance.Repositories.Employees;

namespace Linkdev.IKEA.BLL.Services.Employees
{
	internal class EmployeeService : IEmployeeService
	{
		private readonly EmployeeRepository _employeeRepo;

		public EmployeeService(EmployeeRepository employeeRepo)
		{
			_employeeRepo = employeeRepo;
		}

		public IEnumerable<EmployeeDto> GetAllEmployees()
		{
			return _employeeRepo.GetIQueryable().Select(employee => new EmployeeDto()
			{
				Name = employee.Name,
				Email = employee.Email,
				Age= employee.Age,
				Salary= employee.Salary,
				IsActive = employee.IsActive,
				Gender = employee.Gender.ToString(),
				EmployeeType = employee.EmployeeType.ToString()
			}).ToList();
		}

		public EmployeeDetailsDto? GetEmployeeDetails(int id)
		{
			var employee = _employeeRepo.Get(id);

			if (employee is { })
				return new EmployeeDetailsDto()
				{
				   Id = employee.Id,
				   Name = employee.Name,
				   Email = employee.Email,
				   Address = employee.Address,
				   PhoneNumber = employee.PhoneNumber,
				   IsActive= employee.IsActive,
				   Gender = employee.Gender.ToString(),
				   EmployeeType = employee.EmployeeType.ToString(),
				   Salary = employee.Salary,
				   Age = employee.Age,
				   HiringDate = employee.HiringDate,
				};

			return null;
		}

		public int CreateEmployee(CreatedEmployeeDto employee)
		{
			var newEmployee = new Employee()
			{
				Name = employee.Name,
				Age = employee.Age,
				Email = employee.Email,
				Address = employee.Address,
				PhoneNumber = employee.PhoneNumber,
				Salary = employee.Salary,
				IsActive = employee.IsActive,
				HiringDate = employee.HiringDate,
				Gender = Enum.Parse<Gender>(employee.Gender),
				EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
				CreatedBy = 1,
				LastModifiedBy = 1,
				CreatedOn = DateTime.UtcNow,
				LastModifiedOn = DateTime.UtcNow,
			};

			return _employeeRepo.Add(newEmployee);
		}

		public int UpdateEmployee(UpdatedEmployeeDto employee)
		{
			var newEmployee = new Employee()
			{
				Id = employee.Id,
				Name = employee.Name,
				Age = employee.Age,
				Email = employee.Email,
				Address = employee.Address,
				PhoneNumber = employee.PhoneNumber,
				Salary = employee.Salary,
				IsActive = employee.IsActive,
				HiringDate = employee.HiringDate,
				Gender = Enum.Parse<Gender>(employee.Gender),
				EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
				CreatedBy = 1,
				LastModifiedBy = 1,
				CreatedOn = DateTime.UtcNow,
				LastModifiedOn = DateTime.UtcNow,
			};

			return _employeeRepo.Update(newEmployee);
		}

		public bool DeleteEmployee(int id)
		{
			var employee = _employeeRepo.Get(id);

			if(employee is { })
			return _employeeRepo.Delete(employee) > 0;

			return false;
		}
	}
}
