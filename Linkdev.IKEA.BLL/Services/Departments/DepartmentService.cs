using Linkdev.IKEA.BLL.Models.Departments;
using Linkdev.IKEA.DAL.Entities.Departments;
using Linkdev.IKEA.DAL.Presistance.Repositories.Departments;

namespace Linkdev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentService(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepo.GetIQueryable().Where(D => !D.IsDeleted).Select(D => new { D.Id, D.Code, D.Name, D.CreationDate });

            foreach(var department in departments)
            {
                yield return new DepartmentDto
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    CreationDate = department.CreationDate,
                };
            } 
                
        }

        public DepartmentDetailsDto? GetDepartmentDetails(int id)
        {
            var department = _departmentRepo.Get(id);




            if (department is { })
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn
                };

            return null;
        }

        public int CreateDepartment(CreatedDepartmentDto department)
        {
            Department newDeptartment = new Department()
            { 
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
            };

            

            return _departmentRepo.Add(newDeptartment); 
        }
        public int UpdateDepartment(UpdatedDepartmentDto department)
        {
            Department updatedDeptartment = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
            };

            return _departmentRepo.Update(updatedDeptartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepo.Get(id);

            if (department is { })
                return _departmentRepo.Delete(department) > 0;

            return false;
        }
    }
}
