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

        public EmocionViewModel(KeyValuePair<string, float> prediccion)
        {
            this.Nombre = prediccion.Key;
            this.Porcentaje = prediccion.Value * 100;
        }


        public static IList<EmocionViewModel> Map(IOrderedEnumerable<KeyValuePair<string, float>> prediccion)
        {
            return prediccion.Select(x => new EmocionViewModel(x)).ToList();
        }
    }
}
