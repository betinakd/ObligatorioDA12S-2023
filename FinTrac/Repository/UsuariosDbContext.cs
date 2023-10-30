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
		public DbSet<EspacioUsuario> EspaciosUsuarios { get; set; }
		public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EspacioUsuario>()
				.HasKey(eu => new
				{
					eu.IdEspacio,
					eu.CorreoUsuario
				});

			modelBuilder.Entity<EspacioUsuario>()
				.HasOne(ue => ue.Usuario)
				.WithMany(u => u.EspaciosUsuarios)
				.HasForeignKey(ue => ue.CorreoUsuario);

			modelBuilder.Entity<EspacioUsuario>()
				.HasOne(ue => ue.Espacio)
				.WithMany(e => e.UsuariosInvitados)
				.HasForeignKey(ue => ue.IdEspacio);

			modelBuilder.Entity<Usuario>()
				.HasKey(u => u.Correo);
			modelBuilder.Entity<Usuario>()
	.HasMany(u => u.EspaciosUsuarios)
	.WithOne(eu => eu.Usuario)
	.HasForeignKey(eu => eu.CorreoUsuario);

			modelBuilder.Entity<Espacio>()
				.HasMany(e => e.UsuariosInvitados)
				.WithOne(eu => eu.Espacio)
				.HasForeignKey(eu => eu.IdEspacio);
			modelBuilder.Entity<Espacio>()
				.HasKey(e => e.Id);
			modelBuilder.Entity<Espacio>()
				.HasMany(c => c.Cambios);

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
