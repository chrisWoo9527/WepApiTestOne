using HIS.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpDownloadController : ControllerBase
    {

        private readonly IConfiguration? _configuration;

        public UpDownloadController(IConfiguration? configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("文件流异常，请检查！");


            var path = Path.Combine(_configuration.GetSection("Ftp").Value, "His", file.FileName);

            using (var steam = new FileStream(path, FileMode.OpenOrCreate))
            {
                await file.CopyToAsync(steam);
            }
            return "ok";
        }


        [HttpPost]
        public async Task<ActionResult> Download(string fileName)
        {
            if (fileName == null)
            {
                return Content("filename not present");
            }

            var path = Path.Combine(_configuration.GetSection("Ftp").Value, "His", fileName);

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, FileHelper.GetContentType(path), Path.GetFileName(path));



        }
    }
}

