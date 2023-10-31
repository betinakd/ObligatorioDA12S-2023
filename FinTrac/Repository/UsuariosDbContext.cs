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
		public DbSet<Ahorro> Ahorros { get; set; }
		public DbSet<Credito> Creditos { get; set; }
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
			   .HasForeignKey(ue => ue.CorreoUsuario)
			   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<EspacioUsuario>()
				.HasOne(ue => ue.Espacio)
				.WithMany(e => e.UsuariosInvitados)
				.HasForeignKey(ue => ue.IdEspacio)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Usuario>()
				.HasKey(u => u.Correo);

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
				.HasKey(c => new { c.EspacioId, c.Nombre });

			modelBuilder.Entity<Transaccion>()
				.HasKey(t => t.IdTransaccion);

			modelBuilder.Entity<Objetivo>()
				.HasOne(o => o.Espacio)
				.WithMany(e => e.Objetivos)
				.HasForeignKey(o => o.EspacioId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Objetivo>()
				.HasKey(o => new { o.Titulo, o.EspacioId });

			modelBuilder.Entity<Objetivo>()
				.HasMany(o => o.Categorias)
				.WithMany(c => c.Objetivos)
				.UsingEntity(j => j.ToTable("ObjetivoCategoria"));


			modelBuilder.Entity<Cuenta>()
				.HasDiscriminator<string>("Tipo_cuenta")
				.HasValue<Ahorro>("Ahorro")
				.HasValue<Credito>("Credito");

			modelBuilder.Entity<Cuenta>()
				.HasKey(c => c.Id);

			modelBuilder.Entity<Cuenta>()
				.Property(c => c.Id)
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<Ahorro>()
				.Property(a => a.Nombre)
				.IsRequired();

			modelBuilder.Entity<Credito>()
				.Property(c => c.BancoEmisor)
				.IsRequired();

			modelBuilder.Entity<Credito>()
				.Property(c => c.NumeroTarjeta)
				.IsRequired();
		}

		public Espacio ObtenerEspacioConMayorId()
		{
			return Espacios.OrderByDescending(e => e.Id).FirstOrDefault();
		}

	}
}
