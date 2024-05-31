using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconocimientoEmocionesIA_Entidades
{
    public class Meme
    {
        public string Imagen { get; set; }

        public string Frase { get; set; }

        public List<Emocion> Emociones { get; set; }
    }
}
