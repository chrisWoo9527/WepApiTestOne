using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace His.Service
{
    public class UpDownloadService : IUpDownloadService
    {
        public Task<string> UploadFile(IFormFile file)
        {
            return Task.FromResult("");
        }
    }
}
