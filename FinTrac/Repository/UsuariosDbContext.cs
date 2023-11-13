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
			modelBuilder.Entity<Espacio>()
			.HasOne(e => e.Admin)
			.WithMany(u => u.EspaciosAdmin)
			.HasForeignKey(e => e.AdminId)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Espacio>()
				.HasMany(e => e.Cambios)
				.WithOne(o => o.Espacio)
				.HasForeignKey(o => o.EspacioId);

			modelBuilder.Entity<Espacio>()
				.HasMany(e => e.Categorias)
				.WithOne(o => o.Espacio)
				.HasForeignKey(o => o.EspacioId);

			modelBuilder.Entity<Espacio>()
				.HasMany(e => e.Cuentas)
				.WithOne(o => o.Espacio)
				.HasForeignKey(o => o.EspacioId);

			modelBuilder.Entity<Espacio>()
				.HasMany(e => e.Transacciones)
				.WithOne(o => o.Espacio)
				.HasForeignKey(o => o.EspacioId);

			modelBuilder.Entity<Espacio>()
				.HasMany(e => e.Objetivos)
				.WithOne(o => o.Espacio)
				.HasForeignKey(o => o.EspacioId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Objetivo>()
				.HasMany(o => o.Categorias)
				.WithMany(c => c.Objetivos)
				.UsingEntity(j => j.ToTable("ObjetivoCategoria"));

			modelBuilder.Entity<Cuenta>()
				.HasDiscriminator<string>("Tipo_cuenta")
				.HasValue<Ahorro>("Ahorro")
				.HasValue<Credito>("Credito");

			modelBuilder.Entity<Transaccion>()
				.HasDiscriminator<string>("Tipo_Transaccion")
				.HasValue<TransaccionCosto>("TransaccionCosto")
				.HasValue<TransaccionIngreso>("TransaccionIngreso");

			modelBuilder.Entity<Categoria>()
				.HasMany(c => c.Transacciones)
				.WithOne(t => t.CategoriaTransaccion)
				.HasForeignKey(t => t.CategoriaId);

			modelBuilder.Entity<Cuenta>()
				.HasMany(c => c.Transacciones)
				.WithOne(t => t.CuentaMonetaria)
				.HasForeignKey(t => t.CuentaId);

			modelBuilder.Entity<Transaccion>()
				.HasOne(t => t.CategoriaTransaccion)
				.WithMany(c => c.Transacciones)
				.HasForeignKey(t => t.CategoriaId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Transaccion>()
				.HasOne(t => t.CuentaMonetaria)
				.WithMany(c => c.Transacciones)
				.HasForeignKey(t => t.CuentaId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Transaccion>()
				.HasOne(t => t.Espacio)
				.WithMany(e => e.Transacciones)
				.HasForeignKey(t => t.EspacioId);

			modelBuilder.Entity<Cuenta>()
				.HasOne(c => c.Espacio)
				.WithMany(e => e.Cuentas)
				.HasForeignKey(c => c.EspacioId);

			modelBuilder.Entity<Categoria>()
				.HasOne(c => c.Espacio)
				.WithMany(e => e.Categorias)
				.HasForeignKey(c => c.EspacioId);
		}
	}
}
