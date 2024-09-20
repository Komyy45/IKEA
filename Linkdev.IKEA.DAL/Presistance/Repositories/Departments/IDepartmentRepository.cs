using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Models.Departments;

namespace Linkdev.IKEA.DAL.Presistance.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withAsNoTracking);

        public IQueryable<Department> GetIQueryable();

        Department? Get(int id);

        int Add(Department department);

        int Update(Department department);

        int Delete(Department department);
    }
}
