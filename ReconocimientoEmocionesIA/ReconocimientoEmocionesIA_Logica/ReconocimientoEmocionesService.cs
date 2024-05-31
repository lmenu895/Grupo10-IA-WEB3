using ReconocimientoEmocionesIA_Entidades;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using System.Linq;

namespace ReconocimientoEmocionesIA_Logica
{
    public class ReconocimientoEmocionesService : IReconocimientoEmocionesService
    {
        public List<Emocion> ListarEmocionesDetectadas(byte[] imagen)
        {
             ReconocimientoEmocionesIAModelML.ModelInput modelInput = new ReconocimientoEmocionesIAModelML.ModelInput()
             {
                 ImageSource = imagen,
             };

            return MapToEmocion(ReconocimientoEmocionesIAModelML.PredictAllLabels(modelInput)).Take(3).ToList(); 
        }

        private static List<Emocion> MapToEmocion(IOrderedEnumerable<KeyValuePair<string, float>> orderedEnumerable)
        {
            List<Emocion> emocionList = new List<Emocion>();

            foreach (var kvp in orderedEnumerable)
            {
                emocionList.Add(new Emocion(kvp.Key, kvp.Value));
            }

            return emocionList;
        }
    }
}
