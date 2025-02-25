namespace CasoPractico1.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        //Objetos relacionados a tablas
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
