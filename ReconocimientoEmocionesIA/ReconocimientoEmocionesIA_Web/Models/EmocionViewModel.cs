using ReconocimientoEmocionesIA_Entidades;
using Tensorflow;

namespace ReconocimientoEmocionesIA_Web.Models
{
    public class EmocionViewModel
    {
        public string Nombre { get; set; }

        public float Porcentaje { get; set; }


        public EmocionViewModel()
        {
        }

        public EmocionViewModel(Emocion prediccion)
        {
            this.Nombre = prediccion.Nombre;
            this.Porcentaje = prediccion.Porcentaje * 100;
        }


        public static IList<EmocionViewModel> Map(List<Emocion> prediccion)
        {
            return prediccion.Select(x => new EmocionViewModel(x)).ToList();
        }
    }
}
