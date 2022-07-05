using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Dictionaries
{
    public enum DepartmentProperty
    {
        全院 = 0,
        门诊 = 1,
        护理 = 2,
        临床 = 3,
        医技 = 4,
        药剂 = 5,
        行政 = 6,
        财务 = 7,
        处置 = 8,
        其它 = 9
    }

    public enum OldFlag
    {
        启用 = 0,
        禁用 = 1
    }

    public enum NewFlag
    {
        启用 = 0,
        禁用 = 1
    }
}
