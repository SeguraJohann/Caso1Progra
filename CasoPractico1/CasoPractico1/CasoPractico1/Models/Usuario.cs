using static CasoPractico1.Models.Enums;

namespace CasoPractico1.Models

{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo {  get; set; }
        public string Telefono { get; set; }
        public string Contrasena {  get; set; }
        public RolUsuario Rol { get; set; }
        public DateTime FechaRegistro { get; set; }

        //Objetos relacionados a tablas
        public ICollection<Ruta>? Rutas { get; set; }
        public ICollection<Vehiculo>? Vehiculos { get; set; }
        public ICollection<Boleto>? Boletos { get; set; }

    }
}
