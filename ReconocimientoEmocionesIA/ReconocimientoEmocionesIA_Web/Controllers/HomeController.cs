using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Logica;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Logica.Servicios;
using ReconocimientoEmocionesIA_Web.Models;

namespace ReconocimientoEmocionesIA_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImagenService imagenService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IMemeService memeService;

        public HomeController(IMemeService memeService, IImagenService imagenService, IWebHostEnvironment hostingEnvironment)
        {
            this.imagenService = imagenService;
            this.hostingEnvironment = hostingEnvironment;
            this.memeService = memeService;
        }

        [HttpGet]
        public IActionResult Index(MemeViewModel memeViewModel)
        {
            return View(memeViewModel);
        }

        [HttpPost]
        public IActionResult GenerarImg(IFormFile imagen)
        {
            try
            {
                var fileName = this.imagenService.GuardarImagen(imagen, this.hostingEnvironment.WebRootPath);

                return RedirectToAction("Index", new MemeViewModel { Imagen = fileName });
            }
            catch (ArgumentException)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult GenerarMeme(string fileName)
        {

            var result = this.memeService.Generar(fileName, this.hostingEnvironment.WebRootPath);

            return View("Index", new MemeViewModel(result));
        }

        [HttpGet]
        public IActionResult Capturar()
        {
            return View();
        }
    }
}