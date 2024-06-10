using System;
using System.Collections.Generic;

namespace ReconocimientoEmocionesIA_Entidades.EF;

public partial class Emotion
{
    public int IdEmotion { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MemeImage> MemeImages { get; set; } = new List<MemeImage>();

    public virtual ICollection<Phrase> Phrases { get; set; } = new List<Phrase>();
}
