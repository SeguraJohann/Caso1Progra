namespace CasoPractico1.Models
{
    public class Boleto
    {
        public int Id { get; set; }
        public int ID_usuario { get; set; }
        public int ID_ruta { get; set; }
        public DateTime FechaCompra { get; set; }
        public bool Estado { get; set; }


        //Llave
        public Ruta? Rutas { get; set; }
        public Usuario? Usuarios { get; set; }
    }
}
