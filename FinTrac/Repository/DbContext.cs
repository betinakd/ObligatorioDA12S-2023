using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository
{
	public class FintracDbContext : DbContext
	{
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Espacio> Espacios { get; set; }
		public FintracDbContext(DbContextOptions<FintracDbContext> options) : base(options)
		{
			if (!Database.IsInMemory())
			{
				Database.Migrate();
			}
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}

		//public Espacio ObtenerEspacioConMayorId()
		//{
		//	return Espacios.OrderByDescending(e => e.Id).FirstOrDefault();
		//}

	}
}

