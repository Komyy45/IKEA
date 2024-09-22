using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.BLL.Models.Departments;
using Linkdev.IKEA.BLL.Models.Employees;

namespace Linkdev.IKEA.BLL.Services.Employees
{
	public interface IEmployeeService
	{
		IEnumerable<EmployeeDto> GetAllEmployees();

		EmployeeDetailsDto? GetEmployeeDetails(int id);

		int CreateEmployee(CreatedEmployeeDto employee);

		int UpdateEmployee(UpdatedEmployeeDto employee);

		bool DeleteEmployee(int id);
	}
}
