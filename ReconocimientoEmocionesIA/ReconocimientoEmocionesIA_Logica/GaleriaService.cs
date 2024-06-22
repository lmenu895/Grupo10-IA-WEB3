using System.Collections.Generic;
using System.Linq;
using ReconocimientoEmocionesIA_Entidades.EF;

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

public interface IGaleriaService
{
    List<MemeImage> ObtenerMemes();
}
