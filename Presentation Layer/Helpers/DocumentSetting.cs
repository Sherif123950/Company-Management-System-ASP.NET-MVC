using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Presentation_Layer.Helpers
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            string FilePath= Path.Combine(FolderPath, FileName);
            using var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);
            return FileName;
        }
        public static void DeleteFile(string FolderName, string FileName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName,FileName);
            if (File.Exists(FilePath))
                File.Delete(FilePath);
            {
                
            }
        }
    }
}
