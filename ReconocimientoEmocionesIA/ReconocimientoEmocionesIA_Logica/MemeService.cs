using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using ReconocimientoEmocionesIA_Entidades;
using ReconocimientoEmocionesIA_Logica.Interfaces;
using ReconocimientoEmocionesIA_Logica.Servicios;

namespace ReconocimientoEmocionesIA_Logica
{
    public class MemeService : IMemeService
    {
        IReconocimientoEmocionesService reconocimientoEmocionesService;
        IImagenService imagenService;

        public MemeService(IImagenService imagenService, IReconocimientoEmocionesService reconocimientoEmocionesService)
        {
            this.reconocimientoEmocionesService = reconocimientoEmocionesService;
            this.imagenService = imagenService;
        }

        // public Meme Generar(string fileName, string webRootPath)
        // {
        //     Meme meme = new Meme();
        //     meme.Imagen= fileName;
        //     meme.Emociones = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(File.ReadAllBytes(this.imagenService.ObtenerPathImagen(fileName, webRootPath)));
        //     meme.Frase = this.ObtenerFrasesAleatorias();

        //     return meme;
        // }

        // public Meme Generar(string fileName, string webRootPath)
        // {
        //     Meme meme = new Meme();
        //     meme.Imagen = fileName;
        //     meme.Emociones = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(File.ReadAllBytes(this.imagenService.ObtenerPathImagen(fileName, webRootPath)));
        //     meme.Frase = this.ObtenerFrasesAleatorias();

        //     string imagePath = this.imagenService.ObtenerPathImagen(fileName, webRootPath);

        //     // Crear una copia de la imagen en memoria para evitar problemas de acceso
        //     using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(imagePath)))
        //     using (Image image = Image.FromStream(memoryStream))
        //     using (Graphics graphics = Graphics.FromImage(image))
        //     using (Font font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel))
        //     {
        //         // Medir el tamaño del texto
        //         SizeF textSize = graphics.MeasureString(meme.Frase, font);
        //         // Calcular la posición centrada
        //         PointF position = new PointF((image.Width - textSize.Width) / 2, (image.Height - textSize.Height) / 2);

        //         // Crear el pincel para el borde sombreado
        //         SolidBrush shadowBrush = new SolidBrush(Color.Black);
        //         // Crear el pincel para el texto
        //         SolidBrush textBrush = new SolidBrush(Color.White);

        //         // Dibujar el borde sombreado
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X - 1, position.Y - 1);
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X + 1, position.Y - 1);
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X - 1, position.Y + 1);
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X + 1, position.Y + 1);

        //         // Dibujar el texto principal
        //         graphics.DrawString(meme.Frase, font, textBrush, position);

        //         // Sobrescribir la imagen original
        //         image.Save(imagePath, ImageFormat.Png);
        //     }

        //     meme.Imagen = imagePath;

        //     return meme;
        // }


        // public Meme Generar(string fileName, string webRootPath)
        // {
        //     Meme meme = new Meme();
        //     meme.Imagen = fileName;
        //     meme.Emociones = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(File.ReadAllBytes(this.imagenService.ObtenerPathImagen(fileName, webRootPath)));
        //     meme.Frase = this.ObtenerFrasesAleatorias();

        //     string imagePath = this.imagenService.ObtenerPathImagen(fileName, webRootPath);
        //     string outputPath = Path.Combine(webRootPath, $"{Path.GetFileNameWithoutExtension(fileName)}_meme{Path.GetExtension(fileName)}");

        //     // Editar la imagen para agregar la frase
        //     using (Image image = Image.FromFile(imagePath))
        //     using (Graphics graphics = Graphics.FromImage(image))
        //     using (Font font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel))
        //     {
        //         // Calcular la posición del texto en la parte superior con margen de 10 píxeles
        //         PointF position = new PointF(10, 10); // Ajusta según tus necesidades

        //         // Crear el pincel para el borde sombreado
        //         SolidBrush shadowBrush = new SolidBrush(Color.Black);
        //         // Crear el pincel para el texto
        //         SolidBrush textBrush = new SolidBrush(Color.White);

        //         // Dibujar el borde sombreado
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X - 1, position.Y - 1);
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X + 1, position.Y - 1);
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X - 1, position.Y + 1);
        //         graphics.DrawString(meme.Frase, font, shadowBrush, position.X + 1, position.Y + 1);

        //         // Dibujar el texto principal
        //         graphics.DrawString(meme.Frase, font, textBrush, position);

        //         // Guarda la imagen editada
        //         image.Save(outputPath, ImageFormat.Png);
        //     }

        //     meme.Imagen = outputPath;

        //     return meme;
        // }



        public Meme Generar(string fileName, string webRootPath)
{
    Meme meme = new Meme();
    meme.Imagen = fileName;
    meme.Emociones = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(File.ReadAllBytes(this.imagenService.ObtenerPathImagen(fileName, webRootPath)));
    meme.Frase = this.ObtenerFrasesAleatorias();

    string imagePath = this.imagenService.ObtenerPathImagen(fileName, webRootPath);

    // Crear una copia de la imagen en memoria para evitar problemas de acceso
    using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(imagePath)))
    using (Image image = Image.FromStream(memoryStream))
    using (Graphics graphics = Graphics.FromImage(image))
    using (Font font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel))
    {
        // Medir el tamaño del texto
        SizeF textSize = graphics.MeasureString(meme.Frase, font);
        // Calcular la posición centrada horizontalmente y en la parte superior de la imagen
        PointF position = new PointF((image.Width - textSize.Width) / 2, 10); // Ajusta la separación desde el borde superior según tus necesidades

        // Crear el pincel para el borde sombreado
        SolidBrush shadowBrush = new SolidBrush(Color.Black);
        // Crear el pincel para el texto
        SolidBrush textBrush = new SolidBrush(Color.White);

        // Dibujar el borde sombreado
        graphics.DrawString(meme.Frase, font, shadowBrush, position.X - 1, position.Y - 1);
        graphics.DrawString(meme.Frase, font, shadowBrush, position.X + 1, position.Y - 1);
        graphics.DrawString(meme.Frase, font, shadowBrush, position.X - 1, position.Y + 1);
        graphics.DrawString(meme.Frase, font, shadowBrush, position.X + 1, position.Y + 1);

        // Dibujar el texto principal
        graphics.DrawString(meme.Frase, font, textBrush, position);

        // Sobrescribir la imagen original
        image.Save(imagePath, ImageFormat.Png);
    }

    meme.Imagen = imagePath;

    return meme;
}


        private string ObtenerFrasesAleatorias()
        {
            var _frases = new List<string>()
            {
                "Hola", "Adios", "Buen dia", "Buenas noches", "Buenas tardes", "Como estas", "Estoy triste", "Estoy feliz", "Estoy enojado", "Estoy sorprendido"
            };
            int index = new Random().Next(_frases.Count);
            return _frases[index];
        }
    }
}
