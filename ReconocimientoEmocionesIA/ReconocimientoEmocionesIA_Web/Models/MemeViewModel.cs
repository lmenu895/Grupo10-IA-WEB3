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
            this.FraseId = meme.Frase.IdPhrase;
            this.EmocionId = meme.Frase.IdEmotion.Value;
            this.Frase = meme.Frase.Description;
            this.Emociones = meme.Emociones.Select(x => new EmocionViewModel(x)).ToList();
        }
    }
}
