using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconocimientoEmocionesIA_Logica.Interfaces;

internal interface IGuardarImagen
{
    Task GuardarImagenAsync(string fileName, string webRootPath);
}
