using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration? _configuration;

        public TestController(IConfiguration? configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<string> GetFile()
        {
            var path = Path.Combine(_configuration.GetSection("Ftp").Value, "ftpFile", "123.txt");
            return Ok(path);
        }
    }
}
