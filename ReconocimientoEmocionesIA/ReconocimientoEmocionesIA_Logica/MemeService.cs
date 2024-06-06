using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using ReconocimientoEmocionesIA_Entidades;
using ReconocimientoEmocionesIA_Entidades.EF;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Logica.Servicios;

namespace ReconocimientoEmocionesIA_Logica
{
    public class MemeService : IMemeService
    {
        IReconocimientoEmocionesService reconocimientoEmocionesService;
        IImagenService imagenService;
        MemeGeneratorContext ctx;

        public MemeService(IImagenService imagenService, IReconocimientoEmocionesService reconocimientoEmocionesService, MemeGeneratorContext ctx) {
            this.reconocimientoEmocionesService = reconocimientoEmocionesService;
            this.imagenService = imagenService;
            this.ctx = ctx;
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
            var result = this.ctx.Phrases.First();
            
            result.Description += "pepito";

            var fraseNueva = new Phrase();

            fraseNueva.Description = "soy nuevo";
            fraseNueva.IdEmotion= 1;
            fraseNueva.Average = 8;

            this.ctx.Add(fraseNueva);

            var fraseABorrar = this.ctx.Phrases.Where(x => x.IdPhrase == 2).First();

            this.ctx.Remove(fraseABorrar);

            this.ctx.SaveChanges();

            return result.Description;

            /*var _frases = new List<string>()
            {
                "Hola", "Adios", "Buen dia", "Buenas noches", "Buenas tardes", "Como estas", "Estoy triste", "Estoy feliz", "Estoy enojado", "Estoy sorprendido"
            };
            int index = new Random().Next(_frases.Count);
                return _frases[index];*/


        }
    }
}
