using Domain;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace RepositoryTest
{
	[TestClass]
	public class UsuariosDbContextTest
	{	
		private UsuariosDbContext _context;
		private UsuarioMemoryRepository _repository;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void Setup()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new UsuarioMemoryRepository(_context);

			_context.Usuarios.Add(new Usuario
			{
				Nombre = "Maxi",
				Apellido = "Gimenez",
				Correo = "maxx@a.com",
				Contrasena = "123456789A",
				Direccion = "address"
			});
			_context.SaveChanges();

			_context.Espacios.Add( new Espacio
			{
				Nombre = "Espacio1",
				Admin = _context.Usuarios.FirstOrDefault(u => u.Correo == "maxx@a.com")
			});
			_context.SaveChanges();

		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Database.EnsureDeleted();
		}

		[TestMethod]
		public void DB_Get_Usuario()
		{
			var usuarios = _repository.Find(u => u.Correo == "maxx@a.com");
			Assert.IsNotNull(usuarios);
			var usuarioInDb = _context.Usuarios.FirstOrDefault(u => u.Correo == "maxx@a.com");
			Assert.AreEqual(usuarioInDb, usuarios);
		}

		[TestMethod]
		public void DB_Get_Espacio()
		{
			var espacio = _context.Espacios.FirstOrDefault(e => e.Nombre == "Espacio1");
			Assert.AreEqual("Espacio1", espacio.Nombre);
		}

		[TestMethod]
		public void UsuariosDbContext_No_Esta_En_Memoria()
		{
			var options = new DbContextOptionsBuilder<UsuariosDbContext>()
				.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EspaciosTest;" +
				" Integrated Security = True; Connect Timeout = 30; Encrypt = False")
				.Options;

			using (var context = new UsuariosDbContext(options))
			{
				Assert.IsFalse(context.Database.IsInMemory());
			}
		}
	}
}

