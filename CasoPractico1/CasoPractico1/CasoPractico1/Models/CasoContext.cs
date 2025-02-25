using Microsoft.EntityFrameworkCore;

namespace CasoPractico1.Models
{
    public class CasoContext : DbContext
    {
        public CasoContext(DbContextOptions<CasoContext> options) : base(options) { }

        public DbSet<Boleto> Boletos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Parada> Paradas { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.HasKey(e => e.Id);
                Usuario.Property(n => n.NombreUsuario).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Boleto>(Boleto =>
            {
                Boleto.HasKey(e => e.Id);
                Boleto.HasOne(ta => ta.Usuarios).WithMany(t => t.Boletos).HasForeignKey(ta => ta.ID_usuario).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Rol>(Rol =>
            {
                Rol.HasKey(e => e.Id);
                Rol.Property(n => n.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Parada>(Parada =>
            {
                Parada.HasKey(e => e.Id);
                Parada.Property(n => n.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Horario>(Horario =>
            {
                Horario.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Vehiculo>(Vehiculo =>
            {
                Vehiculo.HasKey(e => e.Id);
                Vehiculo.Property(n => n.Placa).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Ruta>(Ruta =>
            {
                Ruta.HasKey(e => e.Id);
                Ruta.Property(n => n.Codigo).IsRequired().HasMaxLength(100);
                Ruta.HasOne(ta => ta.Vehiculos).WithMany(t => t.Rutas).HasForeignKey(ta => ta.VehiculoId).OnDelete(DeleteBehavior.Restrict);
            });


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++ Llaves foraneas ++++++++++++++++++++++++++++++++++++++++++++

            //Usuario
            modelBuilder.Entity<Usuario>().HasOne(x => x.Rols).WithMany(e => e.Usuarios).HasForeignKey(f => f.RolId);


            //Boleto
            modelBuilder.Entity<Boleto>().HasOne(x => x.Usuarios).WithMany(e => e.Boletos).HasForeignKey(f => f.ID_usuario);
            modelBuilder.Entity<Boleto>().HasOne(x => x.Rutas).WithMany(e => e.Boletos).HasForeignKey(f => f.ID_ruta);


            //Vehiculo
            modelBuilder.Entity<Vehiculo>().HasOne(x => x.Usuarios).WithMany(e => e.Vehiculos).HasForeignKey(f => f.UsuarioRegistroId);


            //Ruta
            modelBuilder.Entity<Ruta>().HasOne(x => x.Vehiculos).WithMany(e => e.Rutas).HasForeignKey(f => f.VehiculoId);
            modelBuilder.Entity<Ruta>().HasOne(x => x.Usuarios).WithMany(e => e.Rutas).HasForeignKey(f => f.usuarioRegistroId);
            modelBuilder.Entity<Ruta>().HasOne(x => x.Horarios).WithMany(e => e.Rutas).HasForeignKey(f => f.HorarioId);
            modelBuilder.Entity<Ruta>().HasOne(x => x.Paradas).WithMany(e => e.Rutas).HasForeignKey(f => f.ParadaId);


        }


    }
}
