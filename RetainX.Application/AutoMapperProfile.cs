using AutoMapper;
using RetainX.Core;

namespace RetainX.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeInfo, EmployeeStats>();
        }
    }
}
