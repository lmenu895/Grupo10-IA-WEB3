using ReconocimientoEmocionesIA_Entidades;

namespace ReconocimientoEmocionesIA_Logica.Interfaces
{
    public interface IReconocimientoEmocionesService
    {
        List<Emocion> ListarEmocionesDetectadas(byte[] imagen);
    }
}
