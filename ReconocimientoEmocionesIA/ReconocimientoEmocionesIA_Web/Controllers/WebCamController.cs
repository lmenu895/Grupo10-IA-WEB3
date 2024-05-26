using Microsoft.AspNetCore.Mvc;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Web.Models;

namespace ReconocimientoEmocionesIA_Web.Controllers;

public class WebCamController : Controller
{
    // GET
    public IActionResult Captura()
    {
        return View();
    }
    
   [HttpPost]
    public async Task<IActionResult> GuardarImagen([FromBody] ImagenData imagenData)
    {
        try
        {
            var imagenBytes = Convert.FromBase64String(imagenData.ImageData.Split(',')[1]);
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            var nombreArchivo = $"captura_{DateTime.Now:yyyyMMddHHmmss}.png";
            var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

            await System.IO.File.WriteAllBytesAsync(rutaArchivo, imagenBytes);

            return Ok(new { Mensaje = "Imagen guardada con éxito", Ruta = rutaArchivo });
            
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al guardar la imagen: {ex.Message}");
        }
    }
}

public class ImagenData
{
    public string ImageData { get; set; }
}

 
      