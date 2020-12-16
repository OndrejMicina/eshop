using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class FileUpload
    {
        IHostingEnvironment Env;

        public FileUpload(IHostingEnvironment env)
        {
            this.Env = env;
        }
        public async Task<bool> FileUploadAsync(Carousel carousel)
        {
            bool uploadSuccess = false;
            var img = carousel.Image;
            if (img != null && img.ContentType.ToLower().Contains("image") && img.Length > 0 && img.Length < 2_000_000)
            {
                var fileName = Path.GetFileName(img.FileName);
                var fileExtension = Path.GetExtension(img.FileName);
                var fileNameGenerated = Path.GetRandomFileName();
                var fileRelative = Path.Combine("images", "Carousels", fileName + fileExtension);
                var filePath = Path.Combine(Env.WebRootPath, fileRelative);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                carousel.ImageSrc = $"/{fileRelative}";
                uploadSuccess = true;
            }
            return uploadSuccess;
        }
    }
}
