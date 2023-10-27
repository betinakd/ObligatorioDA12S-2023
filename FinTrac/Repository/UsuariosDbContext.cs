using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository
{
	public class UsuariosDbContext : DbContext
	{
		public DbSet<Usuario> Usuarios { get; set; }

		public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Usuario>()
				.HasKey(u => u.Correo);
		}
	}
}
