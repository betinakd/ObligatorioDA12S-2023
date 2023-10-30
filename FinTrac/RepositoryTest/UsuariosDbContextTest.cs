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

			Espacio espacio = new Espacio
			{
				Nombre = "Espacio1",
				Admin = _context.Usuarios.FirstOrDefault(u => u.Correo == "a@a.com"),
			};

			_context.Espacios.Add(espacio);
			_context.SaveChanges();

		}

		[TestMethod]
		public void DB_Get_Usuario()
		{
			var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == "a@a.com");
			Assert.AreEqual("a@a.com", usuario.Correo);
		}

		[TestMethod]
		public void DB_Get_Espacio()
		{
			var espacio = _context.Espacios.FirstOrDefault(e => e.Nombre == "Espacio1");
			Assert.AreEqual("Espacio1", espacio.Nombre);
		}
	}
}

