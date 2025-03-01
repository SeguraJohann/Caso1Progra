using Microsoft.VisualBasic;

namespace CasoPractico1.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public bool Estado { get; set; }
        public int EspaciosDisponibles { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int usuarioRegistroId { get; set; }
        public int VehiculoId { get; set; }
        public int ParadaId { get; set; }
        public int HorarioId { get; set; }


        //Llave
        public Usuario? Usuarios { get; set; }
        public Vehiculo? Vehiculos { get; set; }
        public Parada? Paradas { get; set; }
        public Horario? Horarios { get; set; }


        //Objetos relacionados a tablas
        public ICollection<Boleto>? Boletos { get; set; }
    }
}
