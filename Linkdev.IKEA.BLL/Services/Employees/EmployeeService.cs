using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.BLL.Models.Employees;
using Linkdev.IKEA.DAL.Entities.Common.Enums;
using Linkdev.IKEA.DAL.Entities.Employees;
using Linkdev.IKEA.DAL.Presistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.IKEA.BLL.Services.Employees
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepo;

		public EmployeeService(IEmployeeRepository employeeRepo)
		{
			_employeeRepo = employeeRepo;
		}

		public IEnumerable<EmployeeDto> GetEmployees(string searchValue)
		{
			if(string.IsNullOrEmpty(searchValue)) 
				searchValue = string.Empty;


			return _employeeRepo.GetIQueryable()
				.Where(E => EF.Functions.Like(E.Name, $"%{searchValue}%"))
				.Select(employee => new EmployeeDto()
			{
				Id = employee.Id,
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
				   Gender = employee.Gender,
				   EmployeeType = employee.EmployeeType,
				   Salary = employee.Salary,
				   Age = employee.Age,
				   HiringDate = employee.HiringDate,
				   CreatedBy = employee.CreatedBy,
				   CreatedOn = employee.CreatedOn,
				   LastModifiedBy = employee.LastModifiedBy,
				   LastModifiedOn = employee.LastModifiedOn	
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
				EmployeeType = employee.EmployeeType,
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
