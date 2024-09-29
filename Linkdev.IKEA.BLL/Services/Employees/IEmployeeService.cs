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
		Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string searchValue = null!);

		Task<EmployeeDetailsDto?> GetEmployeeDetailsAsync(int id);

		Task<int> CreateEmployeeAsync(CreatedEmployeeDto employee);

		Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employee);

		Task<bool> DeleteEmployeeAsync(int id);
	}
}
