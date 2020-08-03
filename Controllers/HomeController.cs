using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImageCrop.Models;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace ImageCrop.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new UploadViewModel());
        }

        [HttpPost]
        public IActionResult Index(UploadViewModel model){
            var img = model.MyImage;
            
            Image GetBitmapFromImage(IFormFile file)
            {
                using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    return Image.FromStream(target);
                }
            }
            try{
                Image image = GetBitmapFromImage(model.MyImage); 
                ImageConvert conv = new ImageConvert(new Bitmap(image));
                conv.ConvertImage(model.shape, model.scale).Save("wwwroot/Images/Cropped.jpg" );
            } catch (Exception e) {
                _logger.LogError(e.Message);
                return RedirectToAction("Error");
            }
            
            return RedirectToAction("ViewImage");
        }


        public IActionResult ViewImage() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
