using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
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
            meme.Frase = ObtenerFrasePorEmocion(meme.Emociones.First().Nombre);

            return meme;
        }


        private string ObtenerFrasePorEmocion(string emocion)
        {
            return this.ctx.Phrases
            .Where(x => x.IdEmotionNavigation.Name == emocion)
            .AsEnumerable()
            .OrderBy(e => Guid.NewGuid())
            .First().Description;
        }

        private void guardarMeme(string imagen)
        {
            //var memes = this.ctx.Meme;
            //var meme = new Meme();
            //meme.Imagen = imagen;
            //meme.idFrase = 1
            //memes.add(meme);
            //Meme.savechanges();
        }
    }
}
