using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Departments;
using Linkdev.IKEA.DAL.Presistance.Repositories._Generic;

namespace Linkdev.IKEA.DAL.Presistance.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }
}
