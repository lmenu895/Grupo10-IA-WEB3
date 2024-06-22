using System;
using System.Collections.Generic;

namespace ReconocimientoEmocionesIA_Web.EF;

public partial class Phrase
{
    public int IdPhrase { get; set; }

    public string Description { get; set; } = null!;

    public int? IdEmotion { get; set; }

    public int? Average { get; set; }

    public virtual Emotion? IdEmotionNavigation { get; set; }

    public virtual ICollection<MemeImage> MemeImages { get; set; } = new List<MemeImage>();
}
