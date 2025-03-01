namespace CasoPractico1.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public DateTime horainicio { get; set; }
        public DateTime horafin { get; set; }


        //Objetos relacionados a tablas
        public ICollection<Ruta>? Rutas { get; set; }
    }
}
