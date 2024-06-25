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
        meme.Imagen = this.EscribirFraseEnImagen(imagenPath, meme.Frase.Description);

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
        using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(imagePath)))
        using (Image image = Image.FromStream(memoryStream))
        using (Graphics graphics = Graphics.FromImage(image))
        {
            int baseFontSize = 100;
            float fontSize = baseFontSize * (image.Width / 1920f);

            using (Font font = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                SizeF textSize = graphics.MeasureString(frase, font);
                PointF position = new PointF((image.Width - textSize.Width) / 2, 10); 

                SolidBrush shadowBrush = new SolidBrush(Color.Black);

                SolidBrush textBrush = new SolidBrush(Color.White);

                graphics.DrawString(frase, font, shadowBrush, position.X - 1, position.Y - 1);
                graphics.DrawString(frase,
                                    font,
                                    shadowBrush,
                                    position.X + 1,
                                    position.Y - 1);
                graphics.DrawString(frase, font, shadowBrush, position.X - 1, position.Y + 1);
                graphics.DrawString(frase, font, shadowBrush, position.X + 1, position.Y + 1);

                graphics.DrawString(frase, font, textBrush, position);

                ImageFormat imageFormat = GetImageFormat(imagePath);

                image.Save(imagePath, imageFormat);
            }
        }
        return imagePath;
    }

    private ImageFormat GetImageFormat(string imagePath)
    {
        string extension = Path.GetExtension(imagePath).ToLowerInvariant();
        switch (extension)
        {
            case ".bmp":
                return ImageFormat.Bmp;
            case ".png":
                return ImageFormat.Png;
            case ".tiff":
            case ".tif":
                return ImageFormat.Tiff;
            default:
                return ImageFormat.Jpeg;
        }
    }
}
