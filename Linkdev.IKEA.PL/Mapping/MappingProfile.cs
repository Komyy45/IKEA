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

            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>()
                .ForMember(D => D.Id, (options) => options.MapFrom((src, dist, distMembers, context) => int.Parse((string)(context.Items.ContainsKey("Id") ? context.Items["Id"] : "0"))));

            #endregion

            #region Employee

            CreateMap<EmployeeViewModel, UpdatedEmployeeDto>()
                .ForMember(E => E.Id, (options) => options.MapFrom((src, dist, distMembers, context) => int.Parse((string)(context.Items.ContainsKey("Id") ? context.Items["Id"] : "0"))));

            CreateMap<EmployeeViewModel, CreatedEmployeeDto>();

            CreateMap<EmployeeDetailsDto, EmployeeViewModel>(); 

            #endregion
        }
    }
}
