using Domain;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace RepositoryTest
{
	[TestClass]
	public class UsuariosDbContextTest
	{
		private UsuariosDbContext _context;

		[TestInitialize]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<UsuariosDbContext>()
				.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EspaciosTest; Integrated Security = True; Connect Timeout = 30; Encrypt = False").Options;

			_context = new UsuariosDbContext(options);
			_context.Database.EnsureDeleted();
			_context.Database.EnsureCreated();

			_context.Usuarios.Add(new Usuario
			{
				Nombre = "Maxi",
				Apellido = "Gimenez",
				Correo = "a@a.com",
				Contrasena = "123456789A",
				Direccion = "address"
			});
			_context.SaveChanges();
		}

		[TestMethod]
		public void DB_Get_Usuario()
		{

			var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == "a@a.com");
			Assert.AreEqual("a@a.com", usuario.Correo);
		}
		
		
	}
}

