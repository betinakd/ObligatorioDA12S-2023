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
		private IRepository<Usuario> _repositorioUsuario;
		private UsuarioLogic _usuarioLogic;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
		private IRepository<Espacio> _repositorioEspacio;
		private EspacioLogic _espacioLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = _contextFactory.CreateDbContext();
			_repositorioUsuario = new UsuarioMemoryRepository(_context);
			_usuarioLogic = new UsuarioLogic(_repositorioUsuario);
			_repositorioEspacio = new EspacioMemoryRepository(_context);
			_espacioLogic = new EspacioLogic(_repositorioEspacio);

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
		}

		[TestMethod]
		public void ControladorRegistro_Tiene_UsuarioLogic()
		{
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorRegistro_Registra_Nuevo_Usuario_UsuarioLogic()
		{
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			controladorTest.RegistrarUsuario("test@gmail.com", "Alberto", "Lopez", "HOLAhola123", "Direccion 123");
			Usuario usuarioRegistrado = _usuarioLogic.FindUsuario("test@gmail.com");
			Assert.IsNotNull(usuarioRegistrado);
		}

		[TestMethod]
		public void ControladorRegistro_Tiene_EspacioLogic()
		{
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic,_espacioLogic);
			Assert.IsNotNull(controladorTest);
		}
	}
}
