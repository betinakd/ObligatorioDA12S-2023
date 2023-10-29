using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository
{
	public class UsuariosDbContext : DbContext
	{
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Espacio> Espacios { get; set; }
		public DbSet<Cambio> Cambios { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Cuenta> Cuentas { get; set; }
		public DbSet<Transaccion> Transacciones { get; set; }
		public DbSet<Objetivo> Objetivos { get; set; }

		public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Usuario>()
				.HasKey(u => u.Correo);

			modelBuilder.Entity<Espacio>()
				.HasKey(e => e.Id);
			modelBuilder.Entity<Espacio>()
				.HasMany(c => c.Cambios);
			//modelBuilder.Entity<Espacio>()
			//	.HasMany(e => e.Cambios)
			//	.WithOne(c => c.Espacio)  // La propiedad de navegación en la entidad Cambio
			//	.HasForeignKey(c => c.EspacioId);  // La clave foránea en la entidad Cambio

			modelBuilder.Entity<Cambio>()
					.HasKey(c => new
					{
						c.FechaDeCambio,
						c.Moneda,
						c.EspacioId
					});

			modelBuilder.Entity<Categoria>()
				.HasKey(c => c.Nombre);

			modelBuilder.Entity<Cuenta>()
						.HasKey(c => c.FechaCreacion);

			modelBuilder.Entity<Transaccion>()
						.HasKey(t => t.IdTransaccion);

			modelBuilder.Entity<Objetivo>()
						.HasKey(o => o.Titulo);
		}

		public Espacio ObtenerEspacioConMayorId()
		{
			return Espacios.OrderByDescending(e => e.Id).FirstOrDefault();
		}

	}
}
