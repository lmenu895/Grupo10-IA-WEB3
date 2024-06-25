using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Entidades.EF;
using ReconocimientoEmocionesIA_Logica.Interfaces;

public class GaleriaController : Controller
{
    private readonly IGaleriaService _galeriaService;

    public GaleriaController(IGaleriaService galeriaService)
    {
        _galeriaService = galeriaService;
    }

    public IActionResult Index()
    {
        List<MemeImage> memes = _galeriaService.ObtenerMemes();
        return View(memes);
    }
}
