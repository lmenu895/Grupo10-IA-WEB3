using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Logica;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Web.Models;
using System;

namespace ReconocimientoEmocionesIA_Web.Controllers;

public class HomeController : Controller
{
    private IReconocimientoEmocionesService reconocimientoEmocionesService;
    private string _imageUploadPath;

    public HomeController(IReconocimientoEmocionesService reconocimiento)
    {
        this.reconocimientoEmocionesService = reconocimiento;
        this._imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
        Directory.CreateDirectory(_imageUploadPath);
    }

    public IActionResult Index()
    {
        return View(new List<EmocionViewModel>());
    }

    /*public IActionResult Index()
    {
        var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\AIVAN\Desktop\PrograWeb3\Imagenes\pngtree-excited-face-of-woman-closeup-people-palm-photo-picture-image_6792572.jpg");
        var result = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(imageBytes);

        return View(EmocionViewModel.Map(result));
    }*/

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
}