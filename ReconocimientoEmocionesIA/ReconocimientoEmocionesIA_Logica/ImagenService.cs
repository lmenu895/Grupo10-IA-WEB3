using Microsoft.AspNetCore.Http;

namespace ReconocimientoEmocionesIA_Logica.Servicios;
public interface IImagenService
{
    string GuardarImagen(IFormFile imagen, string webRootPath);
    public string ObtenerFrasesAleatorias();
}

public class ImagenService : IImagenService
{
    private static Random ran = new Random();
    private List<string> _frases = new List<string>()
    {
        "Hola", "Adios", "Buen dia", "Buenas noches", "Buenas tardes", "Como estas", "Estoy triste", "Estoy feliz", "Estoy enojado", "Estoy sorprendido"
    };

    public string GuardarImagen(IFormFile imagen, string webRootPath)
    {
        if (imagen != null && imagen.Length > 0)
        {
            var uploadsFolder = Path.Combine(webRootPath, "img");

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

    public string ObtenerFrasesAleatorias()
    {
        int index = ran.Next(_frases.Count);
        return _frases[index];
    }
}
