using ReconocimientoEmocionesIA_Entidades;

namespace ReconocimientoEmocionesIA_Web.Models
{
    public class MemeViewModel
    {
        public string Imagen { get; set; }

        public string Frase { get; set; }

        public int EmocionId { get; set; }
        public int FraseId { get; set; }

        public List<EmocionViewModel> Emociones { get; set; }

        public MemeViewModel()
        {
        }

        public MemeViewModel(Meme meme)
        {
            this.Imagen = meme.Imagen;
            this.Frase= meme.Frase;
            this.EmocionId = meme.EmocionId;
            this.FraseId = meme.FraseId;
            this.Emociones = meme.Emociones.Select(x => new EmocionViewModel(x)).ToList();
        }
    }
}
