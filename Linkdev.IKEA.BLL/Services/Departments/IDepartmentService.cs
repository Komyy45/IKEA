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
        IEnumerable<DepartmentDto> GetAllDepartments();

        DepartmentDetailsDto? GetDepartmentDetails(int id);

        int CreateDepartment(CreatedDepartmentDto department);

        int UpdateDepartment(UpdatedDepartmentDto department);

        bool DeleteDepartment(int id);
    }
}
