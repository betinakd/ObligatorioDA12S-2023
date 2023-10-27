using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class EspacioDBContext : DbContext
	{
		
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Espacio> Espacios { get; set; }

		public EspacioDBContext(DbContextOptions<EspacioDBContext> options) : base(options){ }

		
	}
}
