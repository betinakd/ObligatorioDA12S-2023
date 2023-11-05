using BussinesLogic;
using Domain;
using Repository;
using Controlador;
using Microsoft.EntityFrameworkCore;

namespace ControladorTest
{
	[TestClass]
	public class ControladorHomeTest
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
		public void ControladorHome_Tiene_UsuarioLogic()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.UsuarioLogic = _usuarioLogic;
			Assert.IsNotNull(controladorTest.UsuarioLogic);
			Assert.AreEqual(_usuarioLogic, controladorTest.UsuarioLogic);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Nombre()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.Nombre = "Juan";
			Assert.IsNotNull(controladorTest.Nombre);
			Assert.AreEqual("Juan", controladorTest.Nombre);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Apellido()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.Apellido = "Perez";
			Assert.IsNotNull(controladorTest.Apellido);
			Assert.AreEqual("Perez", controladorTest.Apellido);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Direccion()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.Direccion = "street 56 av rety";
			Assert.IsNotNull(controladorTest.Direccion);
			Assert.AreEqual("street 56 av rety", controladorTest.Direccion);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Correo()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.Correo = "test@gmail.com";
		}

		[TestMethod]
		public void ControladorHome_Tiene_Contrasena()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.Contrasena = "HOLAhola123";
		}

		[TestMethod]
		public void ControladorHome_Tiene_Constructor()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorHome_Recibe_UsuarioLogic_Y_Correo_Y_Carga_Nombre_De_Usuario()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.AreEqual("Juan", controladorTest.Nombre);
		}

		[TestMethod]
		public void ControladorHome_Recibe_UsuarioLogic_Y_Correo_Y_Carga_UsuarioLogic()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.AreEqual(_usuarioLogic, controladorTest.UsuarioLogic);
		}

		[TestMethod]
		public void ControladorHome_Recibe_UsuarioLogic_Y_Correo_Y_Carga_Correo()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.AreEqual("hola@gmail.com", controladorTest.Correo);
		}

		[TestMethod]
		public void ControladorHome_Recibe_UsuarioLogic_Y_Correo_Y_Carga_Apellido()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.AreEqual("Perez", controladorTest.Apellido);
		}

		[TestMethod]
		public void ControladorHome_Recibe_UsuarioLogic_Y_Correo_Y_Carga_Direccion()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.AreEqual("street 56 av rety", controladorTest.Direccion);
		}

		[TestMethod]
		public void ControladorHome_Recibe_UsuarioLogic_Y_Correo_Y_Carga_Contrasena()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.AreEqual("123456789Aaa", controladorTest.Contrasena);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Nombre()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.ModificarNombre("Juan");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Juan", usuarioModificado.Nombre);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Apellido()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.ModificarApellido("Nuñez");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Nuñez", usuarioModificado.Apellido);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Contrasena()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.ModificarContrasena("HOLAhola123");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("HOLAhola123", usuarioModificado.Contrasena);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Direccion()
		{
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.ModificarDireccion("Av Bvar españa 3456");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Av Bvar españa 3456", usuarioModificado.Direccion);
		}
	}
}