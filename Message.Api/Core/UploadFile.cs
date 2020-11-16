using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;

namespace Message.Api.Core
{
    public interface IUploadFile
    {
        string UploadFileImage(IFormFile file, Guid ApplicationUserId);
    }
    public class UploadFile : IUploadFile
    {
        public IWebHostEnvironment WebHostEnvironment;
        public UploadFile(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public string UploadFileImage(IFormFile file, Guid ApplicationUserId)
        {
            string filename = null;

            if (file != null)
            {
                string uploadDir = Path.Combine("Images");
                filename = ApplicationUserId.ToString() + file.FileName;
                string filepath = Path.Combine(uploadDir, filename);
                var fileContents = Directory.GetFiles(uploadDir);
                foreach (var item in fileContents)
                {
                    if (item.Contains(ApplicationUserId.ToString()))
                    {
                        File.Delete(item);
                    }
                }
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    filename = fileStream.Name;
                }
            }
            return filename;
        }
        public string GetPathAndFileName(string filename)
        {
            string path = WebHostEnvironment.WebRootPath + "\\Image\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + filename;
        }
    }
}
