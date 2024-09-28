using Microsoft.AspNetCore.Http;

namespace Linkdev.IKEA.BLL.Common.Services.AttachmentService
{
    internal class AttachmentService : IAttachmentService
    {
        private readonly List<string> _extensions = new List<string> { ".jpg", ".png", ".jpeg" };
        private readonly int _maxixmumSize = 2048;

        public string? Upload(IFormFile attachment, string folderName)
        {
            var extension = Path.GetExtension(attachment.FileName);

            if (!_extensions.Contains(extension))
                return null;

            if (attachment.Length > _maxixmumSize)
                return null;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//files", folderName);

            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            var fileName = $"{attachment.FileName}_{Guid.NewGuid()}";

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            attachment.CopyTo(fileStream);

            return fileName;
        }
        public bool Delete(string attachmentPath)
        {
            if(File.Exists(attachmentPath))
            {
                File.Delete(attachmentPath);
                return true;
            }

            return false;
        }
    }
}
