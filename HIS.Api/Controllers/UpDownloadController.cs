using HIS.Common.FileManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpDownloadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;

        public UpDownloadController(IConfiguration configuration, IFileService fileService)
        {
            _configuration = configuration;
            _fileService = fileService;
        }

        /// <summary>
        /// 文件上传下载
        /// </summary>
        /// <param name="file">文件流</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("文件流异常，请检查！");

            string? FtpPath = _configuration.GetSection("Ftp").Value;

            if (string.IsNullOrEmpty(FtpPath))
            {
                return NotFound("Ftp配置文件异常");
            }

            string path = Path.Combine(FtpPath, file.FileName);


            var directoryPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);


            using (var steam = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await file.OpenReadStream().CopyToAsync(steam);
            }
            return "Ok";
        }


        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileName">文件名称(含后缀)</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Download(string fileName)
        {
            if (fileName == null)
            {
                return Content("filename not present");
            }

            var path = Path.Combine(_configuration.GetSection("Ftp").Value, fileName);

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, FileContentType.GetContentType(path), fileName);
        }


        [HttpGet]
        public ActionResult<List<FileInformation>> SelectFiles()
        {
            List<FileInformation> list = new List<FileInformation>();
            var fileList = Directory.GetFiles(Path.Combine(_configuration.GetSection("Ftp").Value)).ToList();
            fileList.ForEach(file =>
            {
                list.Add(_fileService.GetFileInformation(file));
            });
            return list;
        }
    }
}

