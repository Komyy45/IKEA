using Linkdev.IKEA.BLL.Models.Departments;
using Linkdev.IKEA.DAL.Entities.Departments;
using Linkdev.IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
           return await _unitOfWork.DepartmentRepository.GetIQueryable().Where(D => !D.IsDeleted)
                .Select(D => new DepartmentDto() 
                { 
                    Id= D.Id,
                    Code= D.Code, 
                    Name =D.Name, 
                    CreationDate = D.CreationDate 
                }).ToListAsync();
        }

        public async Task<DepartmentDetailsDto?> GetDepartmentDetailsAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

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

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto department)
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

            return await _unitOfWork.CompleteAsync();
        }
        public Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto department)
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
        
            return _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

            if (department is { })
                _unitOfWork.DepartmentRepository.Delete(department);

            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
