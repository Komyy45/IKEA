using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Employees;
using Linkdev.IKEA.DAL.Presistance.Data;
using Linkdev.IKEA.DAL.Presistance.Repositories._Generic;
using Linkdev.IKEA.DAL.Presistance.Repositories.Departments;

namespace Linkdev.IKEA.DAL.Presistance.Repositories.Employees
{
	public class EmployeeRepository : GenericRepository<Employee>,  IEmployeeRepository
	{
		public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) // ASK CLR for Object from ApplicationDbContext
		{ }
	}
}
