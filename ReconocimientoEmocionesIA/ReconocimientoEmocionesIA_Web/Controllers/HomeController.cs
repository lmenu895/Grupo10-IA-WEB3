using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Logica.Servicios;
using ReconocimientoEmocionesIA_Web.Models;

namespace ReconocimientoEmocionesIA_Web.Controllers
{
    public class HomeController : Controller
    {
        private IReconocimientoEmocionesService reconocimientoEmocionesService;
        private readonly IImagenService _imagenService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IReconocimientoEmocionesService reconocimiento, IImagenService imagenService, IWebHostEnvironment hostingEnvironment)
        {
            this.reconocimientoEmocionesService = reconocimiento;
            _imagenService = imagenService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\Usuario\Desktop\Emociones\sorprendido.jpg");
            var result = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(imageBytes);

            return View(EmocionViewModel.Map(result));
        }

        public ActionResult GenerarImg()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerarImg(IFormFile imagen)
        {
            try
            {
                var fileName = _imagenService.GuardarImagen(imagen, _hostingEnvironment.WebRootPath);

                return RedirectToAction("MostrarImagen", new { imagePath = fileName });
            }
            catch (ArgumentException)
            {
                return View("Error");
            }
        }

        public IActionResult MostrarImagen(string imagePath)
        {
            ViewBag.ImagePath = imagePath;
            ViewBag.TextoSuperior = _imagenService.ObtenerFrasesAleatorias();
            ViewBag.TextoInferior = _imagenService.ObtenerFrasesAleatorias();
            return View();
        }
    }
}
