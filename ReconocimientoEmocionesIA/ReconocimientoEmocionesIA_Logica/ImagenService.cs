using Microsoft.AspNetCore.Http;
using ReconocimientoEmocionesIA_Entidades;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using System;

namespace ReconocimientoEmocionesIA_Logica.Servicios;

public class ImagenService : IImagenService
{
    public ImagenService()
    {  
    }

    public string GuardarImagen(IFormFile imagen, string webRootPath)
    {
        if (imagen != null && imagen.Length > 0)
        {
            var uploadsFolder = Path.Combine(webRootPath, "imagenes");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imagen.CopyTo(stream);
            }

            return fileName;
        } 
        else
        {
            throw new ArgumentException("Formato no válido");
        }
    }



    public async Task<string> GuardarImagenWC(string imagenWC)
    {
        var imagenBytes = Convert.FromBase64String(imagenWC.Split(',')[1]);
        var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

        if (!Directory.Exists(rutaCarpeta))
        {
            Directory.CreateDirectory(rutaCarpeta);
        }

        var nombreArchivo = $"captura_{DateTime.Now:yyyyMMddHHmmss}.png";
        var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

        await System.IO.File.WriteAllBytesAsync(rutaArchivo, imagenBytes);

        return nombreArchivo;
    }

    public string ObtenerPathImagen(string fileName, string webRootPath)
    {
        var uploadsFolder = Path.Combine(webRootPath, "imagenes");
        return Path.Combine(uploadsFolder, fileName);
    }
}
