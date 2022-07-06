﻿using HIS.Common.AutoFacManager;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Common.FileManager
{
    public class FileService : IFileService, ISingletonService
    {
        public FileInformation GetFileInformation(string filePath)
        {
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var fileInformation = new FileInformation
            {
                FileName = Path.GetFileName(filePath),
                FileMd5 = stream.GetFileMD5(),
                FileSize = stream.GetFileSize(),
                LastModifyTime = File.GetLastWriteTime(filePath)
            };
            return fileInformation;
        }

    }
}
