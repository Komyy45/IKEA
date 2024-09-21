using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Departments;
using Linkdev.IKEA.DAL.Presistance.Data;
using Linkdev.IKEA.DAL.Presistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.IKEA.DAL.Presistance.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>,  IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext) // ASK CLR for Object from ApplicationDbContext 
		{ }
    }
}
