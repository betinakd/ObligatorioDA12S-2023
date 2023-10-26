using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class EspacioDBContext : DbContext
	{
		
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Espacio> Espacios { get; set; }

		public EspacioDBContext(DbContextOptions<EspacioDBContext> options) : base(options){ }
	}
}
