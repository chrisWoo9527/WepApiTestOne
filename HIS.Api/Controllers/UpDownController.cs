using HIS.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace HIS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpDownController : ControllerBase
    {
        private readonly IConfiguration? _configuration;

        public UpDownController(IConfiguration? configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("文件流异常，请检查！");


            var ss = file.FileName;

            var path = Path.Combine(_configuration.GetSection("Ftp").Value, "ftpFile", file.FileName);
            Console.WriteLine(path);

            using (var steam = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
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

            var path = Path.Combine(_configuration.GetSection("Ftp").Value, "ftpFile", fileName);

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
