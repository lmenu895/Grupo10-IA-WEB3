namespace ReconocimientoEmocionesIA_Logica.Interfaces
{
    public interface IReconocimientoEmocionesService
    {
        IOrderedEnumerable<KeyValuePair<string, float>> ListarEmocionesDetectadas(byte[] imagen);
    }
}
