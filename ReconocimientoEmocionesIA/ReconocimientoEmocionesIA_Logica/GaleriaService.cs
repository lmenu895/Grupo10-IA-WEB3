using ReconocimientoEmocionesIA_Entidades.EF;
using ReconocimientoEmocionesIA_Logica.Interfaces;

public class GaleriaService : IGaleriaService
{
    private readonly MemeGeneratorContext _ctx;

    public GaleriaService(MemeGeneratorContext ctx)
    {
        _ctx = ctx;
    }

    public List<MemeImage> ObtenerMemes()
    {
        return _ctx.MemeImages.OrderBy(e => e.IdEmotion).ToList();
    }
}