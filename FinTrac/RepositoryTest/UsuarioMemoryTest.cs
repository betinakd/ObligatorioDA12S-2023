using Repository;
using Domain;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace RepositoryTest
{
	[TestClass]
	public class UsuarioMemoryRepositoryTest
	{
		private UsuarioMemoryRepository _repository;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void SetUp()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new UsuarioMemoryRepository(_context);
		}

		[TestCleanup]
		public void CleanUp()
		{
			_context.Database.EnsureDeleted();
		}

		[TestMethod]
		public void FindAll_Trae_Todos_Los_Usuarios()
		{
			var usuario = new Usuario
			{
				IdEspacioPrincipal = 1,
				Nombre = "Usuario1",
				Apellido = "Apellido1",
				Correo = "usuario@gmail.com",
				Contrasena = "HOLAhola123",
				Direccion = "Direccion1",
			};
			_context.Usuarios.Add(usuario);
			_context.SaveChanges();

			var usuarios = _repository.FindAll();
			Assert.IsTrue(usuarios.Contains(usuario));
			Assert.AreEqual(1, usuarios.Count);
		}

	}
}
