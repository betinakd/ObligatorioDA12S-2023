using Domain;
using Repository;
namespace RepositoryTest
{
	[TestClass]
	public class UsuarioMemoryTest
	{
		private Usuario _usuario1;
		private Usuario _usuario2;
		private UsuarioMemoryRepository _repository;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void TestInitialize()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new UsuarioMemoryRepository(_context);

			_usuario1 = new Usuario
			{
				Correo = "usuario1@yy.com",
				Contrasena = "123456789A",
				Nombre = "Usuario1",
				Apellido = "1",
				Direccion = "Dir",
			};
			_usuario2 = new Usuario
			{
				Correo = "usuario2@yy.com",
				Contrasena = "123456789B",
				Nombre = "Usuario2",
				Apellido = "2",
				Direccion = "Direccion",
			};
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Database.EnsureDeleted();
		}

		[TestMethod]
		public void Agregar_Usuario()
		{
			_repository.Add(_usuario1);
			var usuarioInDb = _context.Usuarios.First();
			Assert.AreEqual(_usuario1, usuarioInDb);
		}

		[TestMethod]
		public void Actualizar_Usuario()
		{
			_repository.Add(_usuario1);
			_usuario1.Contrasena = "1234567Yuuuuui";
			_repository.Update(_usuario1);
			var usuarioInDb = _context.Usuarios.First();
			Assert.AreEqual("1234567Yuuuuui", usuarioInDb.Contrasena);
		}

		[TestMethod]
		public void Eliminar_Usuario()
		{
			_repository.Add(_usuario1);
			_repository.Delete(_usuario1.Correo);
			var usuarioInDb = _context.Usuarios.FirstOrDefault(u => u.Correo == _usuario1.Correo);
			Assert.IsNull(usuarioInDb);
		}

		[TestMethod]
		public void Buscar_Usuario()
		{
			_repository.Add(_usuario1);
			var usuarioInDb = _repository.Find(u => u.Correo == _usuario1.Correo);
			Assert.IsNotNull(usuarioInDb);
			Assert.AreEqual(_usuario1, usuarioInDb);
		}

		[TestMethod]
		public void Buscar_Todos_Usuarios()
		{
			_context.Usuarios.Add(_usuario1);
			_context.Usuarios.Add(_usuario2);
			_context.SaveChanges();
			var usuarios = _repository.FindAll();
			Assert.IsNotNull(usuarios);
			Assert.AreEqual(2, usuarios.Count);
		}
	}
}