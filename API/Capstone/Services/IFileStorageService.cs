using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Services
{
    interface IFileStorageService
    {
        public string UploadFileToStorage(IFormFile formFile);
    }
}
