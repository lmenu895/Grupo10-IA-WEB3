

var imageBytes = File.ReadAllBytes(@"C:\Users\Fer\Downloads\emocionesData\disgusto\10018.jpg");

var listReconocimiento = Reconocer(imageBytes);

foreach (var score in listReconocimiento)
{
    Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
}
