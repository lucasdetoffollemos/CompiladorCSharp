using Compilador.WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Compilador.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment Environment;

        public HomeController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult Index()
        {
            string path = Path.Combine(Environment.WebRootPath, "~/Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(List<IFormFile> postedFiles)
        {
            string path = Path.Combine(Environment.WebRootPath, "~/Uploads");

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                //string fileName = Path.GetFileName(postedFile.FileName);
                string fileName = "codigo.txt";
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("Arquivo enviado com sucesso.<br />", fileName);
                }
            }

            return View();
        }
    }
}
