using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Linkdev.IKEA.BLL.Common.Services.AttachmentService
{
    internal interface IAttachmentService
    {
        string? Upload(IFormFile attachment, string folderName);

        bool Delete(string attachmentPath);
    }
}
