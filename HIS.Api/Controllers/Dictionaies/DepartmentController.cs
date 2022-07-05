using HIS.Common.DistributeManager;
using HIS.Dictionaries;
using HIS.Dictionaries.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace HIS.Api.Controllers.Dictionaies
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServivce _departmentServivce;
        private readonly IDistributedCacheHelper _distributedCacheHelper;

        public DepartmentController(IDepartmentServivce departmentServivce, IDistributedCacheHelper distributedCacheHelper)
        {
            _departmentServivce = departmentServivce;
            _distributedCacheHelper = distributedCacheHelper;
        }

        [HttpGet("{Aid}/[Action]")]
        public async Task<ActionResult<List<DepartmentDto>>> GetDepartment(long Aid)
        {
            List<DepartmentDto>? outDto = await _distributedCacheHelper.GetOrCreateAsync("Department" + Aid, async (e) =>
            {
                List<DepartmentDto> departmentDtos = _departmentServivce.GetDepartment(Aid);
                return departmentDtos;
            });

            return outDto;

        }
    }
}
