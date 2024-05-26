using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Web.Models;
using System;

namespace ReconocimientoEmocionesIA_Web.Controllers
{
    public class HomeController : Controller
    {
        private IReconocimientoEmocionesService _reconocimientoEmocionesService;

        public HomeController(IReconocimientoEmocionesService reconocimiento)
        {
            _reconocimientoEmocionesService = reconocimiento;
        }

        
        public IActionResult Index()
        {
            //var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\Fer\Downloads\unhappy-woman-contemplating-after-argument-with-her-boyfriend-home-man-is-background.jpg");
            var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\LkrOverlord\Documents\Projects\UNLaM\ProgramacionWeb3\TrabajoPractico\ImagenesTest\mujer_sorprendida_por_mi_chota.jpg");
            var result = _reconocimientoEmocionesService.ListarEmocionesDetectadas(imageBytes);
            Console.WriteLine("Index está siendo ejecutado.");
            return View(EmocionViewModel.Map(result));
        }
        
        
        
    }
}
