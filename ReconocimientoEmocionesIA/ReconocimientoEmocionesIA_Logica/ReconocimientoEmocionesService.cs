using ReconocimientoEmocionesIA_Logica.Interfaces;

namespace ReconocimientoEmocionesIA_Logica
{
    public class ReconocimientoEmocionesService : IReconocimientoEmocionesService
    {
        public IOrderedEnumerable<KeyValuePair<string, float>> ListarEmocionesDetectadas(byte[] imagen)
        {
             ReconocimientoEmocionesIAModelML.ModelInput modelInput = new ReconocimientoEmocionesIAModelML.ModelInput()
            {
                ImageSource = imagen,
            };

            return ReconocimientoEmocionesIAModelML.PredictAllLabels(modelInput);

        }
    }
}
