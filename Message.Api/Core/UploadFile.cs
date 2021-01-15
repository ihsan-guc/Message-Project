using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Api.Core
{
    public interface IUploadFile
    {
        string UploadFileImage(IFormFile file, Guid ApplicationUserId);
        Task<string> FileImageSaveAsync(IFormFile resim);
        Task<string> FileImageCopyAsync(string resim);
        bool ImageDelete(string resim);
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

        public async Task<string> FileImageSaveAsync(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string imageName = image.FileName.UniqueCodeCreate();
                string filePath = Path.Combine(@"wwwroot\Images\", imageName);
                var file = File.Create(filePath);
                await image.CopyToAsync(file);
                file.Close();
                return imageName;
            }
            return null;
        }

        public async Task<string> FileImageCopyAsync(string image)
        {
            string imageName = image.UniqueCodeCreate();
            File.Copy(@"wwwroot\Images\" + image, @"wwwroot\Images\" + imageName);
            return imageName;
        }

        public bool ImageDelete(string image)
        {
            if (image != null)
            {
                string filePathDelete = Path.Combine(@"wwwroot\Images\", image);
                if (File.Exists(filePathDelete) && image != null)
                {
                    File.Delete(filePathDelete);
                    return true;
                }
                return false;
            }
            return true;
        }
    }
    public static class GuidStringCode
    {
        public static string UniqueCodeCreate(this string fileName, string fileExtension = null)
        {
            string uz;
            if (fileExtension != null)
            {
                uz = fileExtension;
            }
            else
            {
                uz = fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
            }
            return Guid.NewGuid().ToString() + uz;
        }
    }
}
