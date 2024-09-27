using System.Runtime.CompilerServices;
using AutoMapper;
using Linkdev.IKEA.BLL.Models.Departments;
using Linkdev.IKEA.BLL.Models.Employees;
using Linkdev.IKEA.PL.ViewModels.Departments;
using Linkdev.IKEA.PL.ViewModels.Employees;

namespace Linkdev.IKEA.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department
            
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();

            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>(); 

            #endregion

            #region Employee

            CreateMap<EmployeeViewModel, UpdatedEmployeeDto>();

            CreateMap<EmployeeViewModel, CreatedEmployeeDto>();

            CreateMap<EmployeeDetailsDto, EmployeeViewModel>(); 

            #endregion
        }
    }
}
