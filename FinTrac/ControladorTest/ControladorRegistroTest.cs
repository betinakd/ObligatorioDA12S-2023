using BussinesLogic;
using Domain;
using Repository;
using Controlador;
using Microsoft.EntityFrameworkCore;

namespace ControladorTest
{
	[TestClass]
	public class ControladorRegistroTest
	{
		private IRepository<Usuario> _repository;
		private UsuarioLogic _usuarioLogic;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void TestInitialize()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new UsuarioMemoryRepository(_context);
			_usuarioLogic = new UsuarioLogic(_repository);
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};

			var usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
			_usuarioLogic.AddUsuario(usuario1);
			_usuarioLogic.AddUsuario(usuario2);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
			_context = null;
			_repository = null;
			_usuarioLogic = null;
		}

		[TestMethod]
		public void ControladorRegistro_Tiene_UsuarioLogic()
		{
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorRegistro_Registra_Nuevo_Usuario_UsuarioLogic()
		{
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic);
			controladorTest.RegistrarUsuario("test@gmail.com", "Alberto", "Lopez", "HOLAhola123", "Direccion 123");
			Usuario usuarioRegistrado = _usuarioLogic.FindUsuario("test@gmail.com");
			Assert.IsNotNull(usuarioRegistrado);
		}
	}
}
