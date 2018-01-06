using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CloudOfImages
    {
        private readonly Account account;
        private Cloudinary cloudinary;
        public CloudOfImages()
        {
            string cloudName = System.Configuration.ConfigurationManager.AppSettings["CloudName"];
            string APIKey = System.Configuration.ConfigurationManager.AppSettings["APIKey"];
            string APISecret = System.Configuration.ConfigurationManager.AppSettings["APISecret"];
            account = new Account(cloudName, APIKey, APISecret);
            cloudinary = new Cloudinary(account);            
        }

        public string UploadImage(HttpPostedFileBase image)
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