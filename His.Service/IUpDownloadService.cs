using Microsoft.AspNetCore.Http;

namespace His.Service
{
    public interface IUpDownloadService
    {
        Task<string> UploadFile(IFormFile file);


    }
}