using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using ReconocimientoEmocionesIA_Entidades;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Logica.Servicios;

namespace ReconocimientoEmocionesIA_Logica
{
    public class MemeService : IMemeService
    {
        IReconocimientoEmocionesService reconocimientoEmocionesService;
        IImagenService imagenService;

        public MemeService(IImagenService imagenService, IReconocimientoEmocionesService reconocimientoEmocionesService) {
            this.reconocimientoEmocionesService = reconocimientoEmocionesService;
            this.imagenService = imagenService;
        }

        public Meme Generar(string fileName, string webRootPath)
        {
            Meme meme = new Meme();
            meme.Imagen= fileName;
            meme.Emociones = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(File.ReadAllBytes(this.imagenService.ObtenerPathImagen(fileName, webRootPath)));
            meme.Frase = this.ObtenerFrasesAleatorias();

            return meme;
        }

        private string ObtenerFrasesAleatorias()
        {
            var _frases = new List<string>()
            {
                "Hola", "Adios", "Buen dia", "Buenas noches", "Buenas tardes", "Como estas", "Estoy triste", "Estoy feliz", "Estoy enojado", "Estoy sorprendido"
            };
            int index = new Random().Next(_frases.Count);
                return _frases[index];
        }
    }
}
