using System.Collections.Generic;
using System.Linq;

namespace API.Helpers
{
    public class FileValidator
    {
        private static readonly IEnumerable<string> allowededFileTypes = new List<string>()
        {
            ".jpg",
            ".png",
            "jpeg"
        };
        
        public static bool ValidateImageUpload(string extension)
        {
            return allowededFileTypes.Contains(extension);
        }
    }
}