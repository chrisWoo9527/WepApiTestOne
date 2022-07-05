using AutoMapper;
using HIS.Data.DictionariesModel;
using HIS.Dictionaries.Dto;

namespace HIS.Api
{
    public class HisModleMapper : Profile
    {
        public HisModleMapper()
        {
            CreateMap<Department, DepartmentDto>(); 
        }
    }
}
