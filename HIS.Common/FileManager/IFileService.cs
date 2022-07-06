using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Common.FileManager
{
    public interface IFileService
    {
        FileInformation GetFileInformation(string filePath);

        string GetMD5HashFromFile(string file);

        string GetFileSize(string file);
    }
}
