using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Logica;
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
        private string _imageUploadPath;


        public HomeController(IReconocimientoEmocionesService reconocimiento, IImagenService imagenService, IWebHostEnvironment hostingEnvironment)
        {
            this.reconocimientoEmocionesService = reconocimiento;
            _imagenService = imagenService;
            _hostingEnvironment = hostingEnvironment;
            this._imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
            Directory.CreateDirectory(_imageUploadPath);
        }

    public IActionResult Index()
    {
        return View(new List<EmocionViewModel>());
    }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError("image", "Please upload a valid image.");
                return View("Index", new List<EmocionViewModel>());
            }

            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(_imageUploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imageBytes = System.IO.File.ReadAllBytes(filePath);
            var result = reconocimientoEmocionesService.ListarEmocionesDetectadas(imageBytes);

            return View("Index", EmocionViewModel.Map(result));
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