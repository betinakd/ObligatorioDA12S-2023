using BussinesLogic;
using Domain;
using Repository;
using Controlador;
using DTO;
using System.ComponentModel;

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


			var usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};

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
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Constructor()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Nombre()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			string mensaje = "";
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			mensaje = controladorTest.ModificarNombre("Juan", "hola@gmail.com");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Juan", usuarioModificado.Nombre);
			Assert.AreEqual("Su nombre ha sido modificado correctamente.", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_No_Debe_Modificar_Nombre_Incorrecto()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			string mensaje = "";
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			mensaje = controladorTest.ModificarNombre("", "hola@gmail.com");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Juan", usuarioModificado.Nombre);
			Assert.AreEqual("El nombre es requerido", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Apellido()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			string mensaje = controladorTest.ModificarApellido("Nuñez", "hola@gmail.com");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Nuñez", usuarioModificado.Apellido);
			Assert.AreEqual("Su apellido ha sido modificado correctamente.", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_No_Debe_Modificar_Apellido_Incorrecto()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			string mensaje = controladorTest.ModificarApellido("", "hola@gmail.com");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Perez", usuarioModificado.Apellido);
			Assert.AreEqual("El apellido es requerido", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Contrasena()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			string mensaje = controladorTest.ModificarContrasena("HOLAhola123", "hola@gmail.com");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("HOLAhola123", usuarioModificado.Contrasena);
			Assert.AreEqual("Su contraseña ha sido modificado correctamente.", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_No_Debe_Modificar_Contrasena_Incorrecta()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			string mensaje = controladorTest.ModificarContrasena("H", "hola@gmail.com");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("123456789Aaa", usuarioModificado.Contrasena);
			Assert.AreEqual("La contraseña no es válida, debe contener al menos una mayúscula, largo mayor igual a 10 y menor igual a 30", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Direccion()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			string mensaje = controladorTest.ModificarDireccion("Av Bvar españa 3456", "hola@gmail.com");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Av Bvar españa 3456", usuarioModificado.Direccion);
			Assert.AreEqual("Su dirección ha sido modificado correctamente.", mensaje);
		}

		[TestMethod]
		public void ControladorHome_Da_Datos_UsuarioDTO_Con_Correo()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic);
			UsuarioDTO usuarioDTO = controladorTest.DarUsuarioDTO("hola@gmail.com");
			Assert.AreEqual("Juan", usuarioDTO.Nombre);
			Assert.AreEqual("Perez", usuarioDTO.Apellido);
			Assert.AreEqual("123456789Aaa", usuarioDTO.Contrasena);
			Assert.AreEqual("street 56 av rety", usuarioDTO.Direccion);
			Assert.AreEqual("hola@gmail.com", usuarioDTO.Correo);
		}
	}
}