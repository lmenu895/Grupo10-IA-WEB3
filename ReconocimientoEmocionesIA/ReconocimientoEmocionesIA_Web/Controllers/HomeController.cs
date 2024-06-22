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

                return RedirectToAction("GenerarMeme", new { fileName });
            }
            catch (ArgumentException)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult GenerarMeme(string fileName)
        {

            var result = this.memeService.Generar(fileName, this.hostingEnvironment.WebRootPath);
            result.Imagen = Path.GetFileName(result.Imagen);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Capturar([FromBody] ImagenViewModel imagenData)
        {
            try
            {
                var fileName = await this.imagenService.GuardarImagenWC(imagenData.ImageData);
                return Ok(new { fileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarMeme(string fileName, int idEmotion, int idPhrase)
        {
            try
            {
                var result = this.memeService.GuardarMeme(fileName, idEmotion, idPhrase);
                return RedirectToAction("Index", new MemeViewModel { Imagen = fileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el meme: {ex.Message}");
            }
        }

    }
}