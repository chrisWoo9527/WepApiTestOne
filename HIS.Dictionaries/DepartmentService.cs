using AutoMapper;
using HIS.Common;
using HIS.Data;
using HIS.Data.DictionariesModel;
using HIS.Dictionaries.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HIS.Common.AutoFacManager;

namespace HIS.Dictionaries
{
    public class DepartmentService : IDepartmentServivce, IScopeService
    {
        private readonly IMapper _mapper;
        private readonly MirDbContext _context;

        public DepartmentService(IMapper mapper, MirDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<DepartmentDto> GetDepartment(long Id)
        {
            var departments = _context.Departments.AsNoTracking().WhereIf(Id != 0, w => w.AID == Id).ToList() ;
            List<DepartmentDto> departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);
            return departmentDtos;
        }
    }
}
