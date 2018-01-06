using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace WebUI.Models
{
    public class ImageCloud
    {
        private readonly Account account;
        private Cloudinary cloudinary;
        public ImageCloud()
        {
            account = new Account("dct9soklp", "424353261861643", "wGmPp5bkaIpG2Bi12c0veeoiRSo");
            cloudinary = new Cloudinary(account);
        }

        public string ImageUpload(HttpPostedFileBase image)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.Uri.ToString();
        }
    }
}