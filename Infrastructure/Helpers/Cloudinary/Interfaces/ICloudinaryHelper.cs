using Microsoft.AspNetCore.Http;

namespace API.helpers.Cloudinary.Interfaces
{
    public interface ICloudinaryHelper
    {
        string UploadImageOrPdfToCloudinary
            (IFormFile file, string subfolderId, string imageOrPdfGuid, string path);

        void DeleteResourceFromCloudinary
            (string publicId, string subFolderId, string path);
    }
}