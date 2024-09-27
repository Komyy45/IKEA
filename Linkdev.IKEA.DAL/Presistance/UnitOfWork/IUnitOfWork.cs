using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Presistance.Repositories.Departments;
using Linkdev.IKEA.DAL.Presistance.Repositories.Employees;

namespace Linkdev.IKEA.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        int Complete();
    }
}
