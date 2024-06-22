using Microsoft.AspNetCore.Http;

namespace ReconocimientoEmocionesIA_Logica.Interfaces
{
    public interface IImagenService
    {
        Task<string> GuardarImagen(IFormFile imagen, string webRootPath);

        public Task<string> GuardarImagenWC(string imagenWC);

        public string ObtenerPathImagen(string fileName, string webRootPath);

    }
}
