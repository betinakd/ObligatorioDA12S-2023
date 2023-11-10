using BussinesLogic;
using Controlador;
using Domain;
using Repository;
using DTO;

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
			var usuario = new UsuarioDTO()
			{
				Correo = "test@gmail.com",
				Nombre = "test",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			controladorTest.RegistrarUsuario(usuario);
			Usuario usuarioRegistrado = _usuarioLogic.FindUsuario("test@gmail.com");
			Assert.IsNotNull(usuarioRegistrado);
		}

		[TestMethod]
		public void ControladorRegistro_Tiene_EspacioLogic()
		{
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorRegistro_Crea_Espacio_Principal()
		{
			var usuario = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "test",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario);
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			
			controladorTest.CrearEspacioPrincipal("test@gmail.com");
			Espacio espacioPrincipal = _espacioLogic.FindEspacio(1);

			Assert.IsNotNull(espacioPrincipal);
			Assert.AreEqual(espacioPrincipal.Nombre, "Principal test");
		}

		[TestMethod]
		public void ControladorRegistro_Tira_Mensaje_Excepcion_DomainUsuario()
		{
			var usuario = new UsuarioDTO()
			{
				Correo = "",
				Nombre = "test",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			string msjError = controladorTest.RegistrarUsuario(usuario);
			Assert.AreEqual(msjError, "El correo electrónico no es válido, debe terminar en .com y tener @ entre carácteres.");
		
		}

		[TestMethod]
		public void ControladorRegistro_Tira_Mensaje_Excepcion_BussinesLogicUsuario()
		{
			var usuario = new UsuarioDTO()
			{
				Correo = "hola@gmail.com",
				Nombre = "test",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			var usuario2 = new UsuarioDTO()
			{
				Correo = "hola@gmail.com",
				Nombre = "teset",
				Apellido = "Pereez",
				Contrasena = "1234356789Aaa",
				Direccion = "stre3et 56 av rety"
			};
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			string msjError = controladorTest.RegistrarUsuario(usuario);
			Assert.AreEqual(msjError, "El usuario ya existe");
		}

		[TestMethod]
		public void UsuarioRegistradoConExito_True()
		{
			var usuario = new UsuarioDTO()
			{
				Correo = "hola@gmail.com",
				Nombre = "test",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			controladorTest.RegistrarUsuario(usuario);
			Assert.IsTrue(controladorTest.RegistradoConExito(usuario));
		}

		[TestMethod]
		public void UsuarioRegistradoConExito_No_Registro_Retorna_False()
		{
			var usuario = new UsuarioDTO()
			{
				Correo = "",
				Nombre = "test",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			ControladorRegistro controladorTest = new ControladorRegistro(_usuarioLogic, _espacioLogic);
			controladorTest.RegistrarUsuario(usuario);
			Assert.IsFalse(controladorTest.RegistradoConExito(usuario));
		}
	}
}
