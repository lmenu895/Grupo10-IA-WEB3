using System;
using System.Collections.Generic;

namespace ReconocimientoEmocionesIA_Web.EF;

public partial class MemeImage
{
    public int IdImage { get; set; }

    public int? IdPhrase { get; set; }

    public string? ImagePath { get; set; }

    public int? IdEmotion { get; set; }

    public virtual Emotion? IdEmotionNavigation { get; set; }

    public virtual Phrase? IdPhraseNavigation { get; set; }
}
