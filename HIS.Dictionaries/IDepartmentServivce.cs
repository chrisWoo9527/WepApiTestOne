using HIS.Dictionaries.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Dictionaries
{
    public interface IDepartmentServivce
    {
        List<DepartmentDto> GetDepartment(long Id);
    }
}
