using System;
using System.Collections.Generic;
using API.helpers.Cloudinary.Interfaces;
using Infrastructure.configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace API.helpers.Cloudinary
{
    public class CloudinaryHelper : ICloudinaryHelper
    {
        private readonly string _cloudName;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public CloudinaryHelper(
            IOptions<CloudinaryConfig> cloudinaryConfig)
        {
            _cloudName = cloudinaryConfig.Value.CloudName;
            _apiSecret = cloudinaryConfig.Value.ApiSecret;
            _apiKey = cloudinaryConfig.Value.ApiKey;
        }

        public string UploadImageOrPdfToCloudinary(IFormFile file, string subfolderId, string imageOrPdfGuid, string path)
        {
            var account = new Account(_cloudName, _apiSecret, _apiKey);
            var cloudinary = new CloudinaryDotNet.Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = path + subfolderId + "/" + imageOrPdfGuid,
                Overwrite = true
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public void DeleteResourceFromCloudinary(string publicId, string subFolderId, string path)
        {
            var account = new Account(_cloudName, _apiSecret, _apiKey);
            var cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var delResParams = new DelResParams()
            {
                PublicIds = new List<string> {path + subFolderId + "/" + publicId}
            };
            cloudinary.DeleteResources(delResParams);
        }
    }
}