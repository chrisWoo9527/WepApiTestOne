using HIS.Common.AutoFacManager;
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
            var fileInformation = new FileInformation
            {
                FileName = Path.GetFileName(filePath),
                FileMd5 = GetMD5HashFromFile(filePath),
                FileSize = GetFileSize(filePath),
                LastModifyTime = File.GetLastWriteTime(filePath)
            };
            return fileInformation;
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="file">文件绝对路径</param>
        /// <returns>MD5值</returns>
        public string GetMD5HashFromFile(string file)
        {
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                MD5 mD5 = MD5.Create();
                byte[] retVal = mD5.ComputeHash(fileStream);
                fileStream.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("获取文件MD5值error:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取文件大小长度
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetFileSize(string file)
        {
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                long fileLength = fileStream.Length;
                if (fileLength < 0)
                {
                    return "0";
                }
                else if (fileLength >= 1024 * 1024 * 1024)  //文件大小大于或等于1024MB    
                {
                    return string.Format("{0:0.00} GB", (double)fileLength / (1024 * 1024 * 1024));
                }
                else if (fileLength >= 1024 * 1024) //文件大小大于或等于1024KB    
                {
                    return string.Format("{0:0.00} MB", (double)fileLength / (1024 * 1024));
                }
                else if (fileLength >= 1024) //文件大小大于等于1024bytes    
                {
                    return string.Format("{0:0.00} KB", (double)fileLength / 1024);
                }
                else
                {
                    return string.Format("{0:0.00} bytes", fileLength);
                };
            }
            catch (Exception ex)
            {
                throw new Exception("获取字节长度异常:" + ex.Message);
            }
        }
    }
}
