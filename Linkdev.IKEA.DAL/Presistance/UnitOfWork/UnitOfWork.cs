using Linkdev.IKEA.DAL.Presistance.Data;
using Linkdev.IKEA.DAL.Presistance.Repositories.Departments;
using Linkdev.IKEA.DAL.Presistance.Repositories.Employees;

namespace Linkdev.IKEA.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDepartmentRepository _departmentRepository = null!;
        private IEmployeeRepository _employeeRepository = null!;
        private readonly ApplicationDbContext _context;

        public IDepartmentRepository DepartmentRepository 
        { 
            get
            {
                if(_departmentRepository is null)
                {
                    _departmentRepository = new DepartmentRepository(_context);
                }
                return _departmentRepository;
            }
        }
        public IEmployeeRepository EmployeeRepository { 
            get
            {
                if(_employeeRepository is null)
                {
                   _employeeRepository = new EmployeeRepository(_context);
                }
                return _employeeRepository;
            }
        }

        
        public UnitOfWork(ApplicationDbContext context) // Ask CLR for object from class ApplicationDbContext
        {
            _context = context;
        }

        
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _context.DisposeAsync();
        }
    }
}
