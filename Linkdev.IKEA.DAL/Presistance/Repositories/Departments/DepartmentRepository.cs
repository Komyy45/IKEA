using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Models.Departments;
using Linkdev.IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.IKEA.DAL.Presistance.Repositories.Departments
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        } // ASK CLR for Object from ApplicationDbContext

        public IEnumerable<Department> GetAll(bool withAsNoTracking)
        {
            if (withAsNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();

            return _dbContext.Departments.ToList();
        }

        public Department? Get(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            _dbContext.Departments.Remove(Get(id));
            return _dbContext.SaveChanges();
        }
    }
}
