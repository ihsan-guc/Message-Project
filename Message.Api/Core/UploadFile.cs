using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
                string filepath = Path.Combine(uploadDir,filename);
                using (var fileStream = new FileStream(filepath,FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return filename;
        }
    }
}
