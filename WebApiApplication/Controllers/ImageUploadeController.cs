using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadeController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public ImageUploadeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //public class FileUploadAPI
        //{
        //    public IFormFile files { get; set; }
        //}
        [HttpPost]
        public async Task<string> Post(IFormFile strFile)
        {
            string uploadFile = "\\Ulpoad\\";
            try {
                if (strFile.FileName.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + uploadFile))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + uploadFile);
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + uploadFile + strFile.FileName))
                    {
                        await strFile.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return uploadFile + strFile.FileName;
                    }
                }
                else
                {
                    return "Failed!";
                }
            }
            catch(Exception ex)
            {
               return ex.Message.ToString();
            }
        }
    }
}
