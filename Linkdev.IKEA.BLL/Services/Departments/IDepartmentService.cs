using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.BLL.Models.Departments;

namespace Linkdev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();

        Task<DepartmentDetailsDto?> GetDepartmentDetailsAsync(int id);

        Task<int> CreateDepartmentAsync(CreatedDepartmentDto department);

        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto department);

        Task<bool> DeleteDepartmentAsync(int id);
    }
}
