using Microsoft.AspNetCore.Http;
using ReconocimientoEmocionesIA_Entidades;

namespace ReconocimientoEmocionesIA_Logica.Interfaces
{
    public interface IMemeService
    {
        Meme Generar(string fileName, string webRootPath);
    }
}
