using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Web.Models;
using System;

namespace ReconocimientoEmocionesIA_Web.Controllers
{
    public class HomeController : Controller
    {
        private IReconocimientoEmocionesService reconocimientoEmocionesService;

        public HomeController(IReconocimientoEmocionesService reconocimiento)
        {
            this.reconocimientoEmocionesService = reconocimiento;
        }

        public IActionResult Index()
        {
            var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\Fer\Downloads\unhappy-woman-contemplating-after-argument-with-her-boyfriend-home-man-is-background.jpg");
            var result = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(imageBytes);

            return View(EmocionViewModel.Map(result));
        }
    }
}
