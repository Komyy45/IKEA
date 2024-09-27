using Linkdev.IKEA.BLL.Models.Departments;
using Linkdev.IKEA.DAL.Entities.Departments;
using Linkdev.IKEA.DAL.Presistance.Repositories.Departments;
using Linkdev.IKEA.DAL.Presistance.UnitOfWork;

namespace Linkdev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetIQueryable().Where(D => !D.IsDeleted).Select(D => new { D.Id, D.Code, D.Name, D.CreationDate });


            foreach (var department in departments)
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
            var department = _unitOfWork.DepartmentRepository.Get(id);

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

            

            _unitOfWork.DepartmentRepository.Add(newDeptartment);

            return _unitOfWork.Complete();
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

            _unitOfWork.DepartmentRepository.Update(updatedDeptartment);
        
            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);

            if (department is { })
                _unitOfWork.DepartmentRepository.Delete(department);

            return _unitOfWork.Complete() > 0;
        }
    }
}
