using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository
{
	public class UsuariosDbContext : DbContext
	{
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Espacio> Espacios { get; set; }

		public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
		{
			if (!Database.IsInMemory())
			{
				Database.Migrate();
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Usuario>()
				.HasKey(u => u.Correo);

			modelBuilder.Entity<Espacio>()
				.HasMany(e => e.UsuariosInvitados);
			
			modelBuilder.Entity<Espacio>()
				.HasKey(e => e.Id);

			modelBuilder.Entity<Espacio>()
				.HasMany(c => c.Cambios);

			modelBuilder.Entity<Cambio>()
					.HasKey(c => new
					{
						c.FechaDeCambio,
						c.Moneda,
						
					});

			modelBuilder.Entity<Categoria>()
				.HasKey(c => c.Nombre);

			modelBuilder.Entity<Cuenta>()
						.HasKey(c => c.FechaCreacion);

			modelBuilder.Entity<Transaccion>()
						.HasKey(t => t.Id);

			modelBuilder.Entity<Objetivo>()
						.HasKey(o => o.Titulo);

			modelBuilder.Entity<Objetivo>()
						.HasMany(o => o.Categorias);
		}
	}
}
