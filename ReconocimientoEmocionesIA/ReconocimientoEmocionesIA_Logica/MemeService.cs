using System.Drawing;
using System.Drawing.Imaging;
using ReconocimientoEmocionesIA_Entidades;
using ReconocimientoEmocionesIA_Entidades.EF;
using ReconocimientoEmocionesIA_Logica.Interfaces;

namespace ReconocimientoEmocionesIA_Logica;

public class MemeService : IMemeService
{
    IReconocimientoEmocionesService reconocimientoEmocionesService;
    IImagenService imagenService;
    MemeGeneratorContext ctx;

    public MemeService(IImagenService imagenService, IReconocimientoEmocionesService reconocimientoEmocionesService, MemeGeneratorContext ctx) {
        this.reconocimientoEmocionesService = reconocimientoEmocionesService;
        this.imagenService = imagenService;
        this.ctx = ctx;
    }

    public Meme Generar(string fileName, string webRootPath)
    {
        var imagenPath = this.imagenService.ObtenerPathImagen(fileName, webRootPath);

        Meme meme = new Meme();
        meme.Emociones = this.reconocimientoEmocionesService.ListarEmocionesDetectadas(File.ReadAllBytes(imagenPath));
        meme.Frase = ObtenerFrasePorEmocion(meme.Emociones.First().Nombre);

        var emocion = meme.Emociones.FirstOrDefault();


        string imagePath = this.imagenService.ObtenerPathImagen(fileName, webRootPath);

        // Crear una copia de la imagen en memoria para evitar problemas de acceso
        using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(imagePath)))
        using (Image image = Image.FromStream(memoryStream))
        using (Graphics graphics = Graphics.FromImage(image))
        using (Font font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel))
        {
            // Medir el tamaño del texto
            SizeF textSize = graphics.MeasureString(meme.Frase.Description, font);
            // Calcular la posición centrada horizontalmente y en la parte superior de la imagen
            PointF position = new PointF((image.Width - textSize.Width) / 2, 10); // Ajusta la separación desde el borde superior según tus necesidades

            // Crear el pincel para el borde sombreado
            SolidBrush shadowBrush = new SolidBrush(Color.Black);
            // Crear el pincel para el texto
            SolidBrush textBrush = new SolidBrush(Color.White);

        return meme;
    }

    public bool GuardarMeme(string fileName, int idEmotion, int idPhrase)
    {
        var memes = this.ctx.MemeImages;

        var meme = new MemeImage
        {
            ImagePath = fileName,
            IdEmotion = idEmotion,
            IdPhrase = idPhrase
        };

        memes.Add(meme);
        this.ctx.SaveChanges();

        return true;
    }


    private Phrase ObtenerFrasePorEmocion(string emocion)
    {
        var frase = this.ctx.Phrases
        .Where(x => x.IdEmotionNavigation.Name == emocion)
        .AsEnumerable()
        .OrderBy(e => Guid.NewGuid())
        .FirstOrDefault();

        if (frase == null)
        {
            return this.ctx.Phrases
          .Where(x => x.IdEmotionNavigation.Name == "felicidad")
          .AsEnumerable()
          .OrderBy(e => Guid.NewGuid())
          .FirstOrDefault();

        }

        return frase;
    }

    private string EscribirFraseEnImagen(string imagePath, string frase)
    {
        // Crear una copia de la imagen en memoria para evitar problemas de acceso
        using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(imagePath)))
        using (Image image = Image.FromStream(memoryStream))
        using (Graphics graphics = Graphics.FromImage(image))
        {
            // Calcular el tamaño de la fuente en función de la resolución de la imagen
            int baseFontSize = 100; // Tamaño base para una resolución estándar
            float fontSize = baseFontSize * (image.Width / 1920f); // Ajustar proporcionalmente al ancho de la imagen

            // Crear la fuente con el tamaño calculado
            using (Font font = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                // Medir el tamaño del texto
                SizeF textSize = graphics.MeasureString(meme.Frase.Description, font);
                // Calcular la posición centrada horizontalmente y en la parte superior de la imagen
                PointF position = new PointF((image.Width - textSize.Width) / 2, 10); // Ajusta la separación desde el borde superior según tus necesidades

                // Crear el pincel para el borde sombreado
                SolidBrush shadowBrush = new SolidBrush(Color.Black);
                // Crear el pincel para el texto
                SolidBrush textBrush = new SolidBrush(Color.White);

                // Dibujar el borde sombreado
                graphics.DrawString(meme.Frase.Description, font, shadowBrush, position.X - 1, position.Y - 1);
                graphics.DrawString(meme.Frase.Description, font, shadowBrush, position.X + 1, position.Y - 1);
                graphics.DrawString(meme.Frase.Description, font, shadowBrush, position.X - 1, position.Y + 1);
                graphics.DrawString(meme.Frase.Description, font, shadowBrush, position.X + 1, position.Y + 1);

                // Dibujar el texto principal
                graphics.DrawString(meme.Frase.Description, font, textBrush, position);

                // Obtener el formato de la imagen a partir de la extensión del archivo
                ImageFormat imageFormat = GetImageFormat(imagePath);

                // Sobrescribir la imagen original
                image.Save(imagePath, imageFormat);
            }
        }


        return imagePath;
    }
    }

}
