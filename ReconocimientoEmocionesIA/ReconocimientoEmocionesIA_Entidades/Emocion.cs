namespace ReconocimientoEmocionesIA_Entidades
{
    public class Emocion
    {
        public string Nombre { get; set; }

        public float Porcentaje { get; set;}

        public Emocion() { }

        public Emocion(string nombre, float porcentaje)
        {
            this.Nombre = nombre;
            this.Porcentaje = porcentaje;
        } 
    }
}
