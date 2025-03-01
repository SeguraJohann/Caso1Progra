using System.ComponentModel.DataAnnotations;
using static CasoPractico1.Models.Enums;

namespace CasoPractico1.Models
{
    public class Vehiculo
    {
        
        public int Id { get; set; }

        public string Placa { get; set; }

        public string Modelo { get; set; }

        public int CapacidadPasajeros { get; set; }

        public EstadoVehiculo Estado { get; set; } 

        public DateTime? FechaRegistro { get; set; }

        public int? UsuarioRegistroId { get; set; }

        //Objetos relacionados a tablas
        public ICollection<Ruta>? Rutas { get; set; }

        //Llave
        public Usuario? Usuarios { get; set; }


    }
}
