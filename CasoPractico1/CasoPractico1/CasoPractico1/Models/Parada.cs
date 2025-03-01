namespace CasoPractico1.Models
{
    public class Parada
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        //Objetos relacionados a tablas
        public ICollection<Ruta>? Rutas { get; set; }
    }
}
