using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.BLL.Models.Employees;
using Linkdev.IKEA.DAL.Entities.Common.Enums;
using Linkdev.IKEA.DAL.Entities.Employees;
using Linkdev.IKEA.DAL.Presistance.Repositories.Employees;
using Linkdev.IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Linkdev.IKEA.BLL.Services.Employees
{
	public class EmployeeService : IEmployeeService
	{
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
        }

		public IEnumerable<EmployeeDto> GetEmployees(string searchValue = null!)
		{
			if(string.IsNullOrEmpty(searchValue)) 
				searchValue = string.Empty;


			return _unitOfWork.EmployeeRepository.GetIQueryable()
				.Include(E => E.Department)
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
				EmployeeType = employee.EmployeeType.ToString(),
                Department =  (employee.Department != null && !employee.Department.IsDeleted ? employee.Department.Name : "No Department")
                }).ToList();
		}

		public EmployeeDetailsDto? GetEmployeeDetails(int id)
		{
			var employee = _unitOfWork.EmployeeRepository.GetIQueryable().Include(E => E.Department).FirstOrDefault(X => X.Id == id);

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
				   LastModifiedOn = employee.LastModifiedOn,
				   Department = (employee.Department is not null && !employee.Department.IsDeleted ? employee.Department.Name : "No Department")
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
				DepartmentId = employee.DepartmentId,
			};

			_unitOfWork.EmployeeRepository.Add(newEmployee);

			return _unitOfWork.Complete();
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
				DepartmentId = employee.DepartmentId
			};

			_unitOfWork.EmployeeRepository.Update(newEmployee);

			return _unitOfWork.Complete();
		}

		public bool DeleteEmployee(int id)
		{
			var employee = _unitOfWork.EmployeeRepository.Get(id);

			if(employee is { })
				_unitOfWork.EmployeeRepository.Delete(employee);

			return _unitOfWork.Complete() > 0;
		}
	}
}
