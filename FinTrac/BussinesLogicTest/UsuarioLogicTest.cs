using Excepcion;
using Domain;
using Repository;
using BussinesLogic;

namespace BussinesLogicTest
{
	[TestClass]
	public class UsuarioLogicTest
	{
		private IRepository<Usuario> _repository;
		private UsuarioLogic _usuarioLogic;
		private Usuario _usuario1;
		private Usuario _usuario2;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void Setup()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new UsuarioMemoryRepository(_context);
			_usuarioLogic = new UsuarioLogic(_repository);
			_usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};

			_usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
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
		public void Nuevo_UsuarioLogic()
		{
			Assert.IsNotNull(_usuarioLogic);
		}

		[TestMethod]
		public void Agregar_Usuario()
		{
			_usuarioLogic.AddUsuario(_usuario1);
			var usuarioAgregado = _repository.Find(u => u.Correo == _usuario1.Correo);
			bool resultado = _usuario1.Equals(usuarioAgregado);
			Assert.IsNotNull(usuarioAgregado);
			Assert.AreEqual(_usuario1.Correo, usuarioAgregado.Correo);
			Assert.AreEqual(_usuario1.Contrasena, usuarioAgregado.Contrasena);
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainUsuarioException))]
		public void Contrasena_invalida_UL()
		{
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.com";
			usuario1.Contrasena = "1234567890";
			_usuarioLogic.AddUsuario(usuario1);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainUsuarioException))]
		public void Correo_invalido_UL()
		{
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.co";
			usuario1.Contrasena = "123456789Aa";
			_usuarioLogic.AddUsuario(usuario1);
		}

		[TestMethod]
		public void Buscar_Todos_Usuarios()
		{

			_usuarioLogic.AddUsuario(_usuario1);
			_usuarioLogic.AddUsuario(_usuario2);
			var usuarios = _usuarioLogic.FindAllUsuario();
			Assert.IsNotNull(usuarios);
			Assert.AreEqual(2, usuarios.Count);
		}

		[TestMethod]
		public void Eliminar_Usuario()
		{
			_usuarioLogic.AddUsuario(_usuario1);
			_usuarioLogic.AddUsuario(_usuario2);
			_usuarioLogic.DeleteUsuario(_usuario1.Correo);
			var usuarios = _usuarioLogic.FindAllUsuario();
			Assert.IsNotNull(usuarios);
			Assert.AreEqual(1, usuarios.Count);
		}

		[TestMethod]
		public void Buscar_Usuario()
		{
			_usuarioLogic.AddUsuario(_usuario1);
			_usuarioLogic.AddUsuario(_usuario2);
			var usuario = _usuarioLogic.FindUsuario(_usuario1.Correo);
			Assert.IsNotNull(usuario);
			Assert.AreEqual(_usuario1.Correo, usuario.Correo);
			Assert.AreEqual(_usuario1.Contrasena, usuario.Contrasena);
		}

		[TestMethod]
		public void Agregar_Usuario_Valido()
		{
			var usuarioAgregado1 = _usuarioLogic.AddUsuario(_usuario1);
			var usuarioAgregado2 = _usuarioLogic.AddUsuario(_usuario2);
			Assert.IsNotNull(usuarioAgregado1);
			Assert.AreEqual(_usuario1, usuarioAgregado1);
			Assert.IsNotNull(usuarioAgregado2);
			Assert.AreEqual(_usuario2, usuarioAgregado2);
		}


		[TestMethod]
		[ExpectedException(typeof(BussinesLogicUsuarioException))]
		public void Agregar_Usuario_Invalido_Duplicado()
		{
			var usuario1 = new Usuario
			{
				Nombre = "Juan",
				Apellido = "Perez",
				Direccion = "street 56 av rety",
				Correo = "Juan@xxxx.com",
				Contrasena = "123456aasaU",
			};
			var usuario2 = new Usuario
			{
				Nombre = "Juan",
				Apellido = "Perez",
				Direccion = "street 56 av rety",
				Contrasena = "123456aasaU",
				Correo = "Juan@xxxx.com"
			};
			_usuarioLogic.AddUsuario(usuario1);
			_usuarioLogic.AddUsuario(usuario2);
		}

		[TestMethod]
		public void Ingreso_Correo_Contrasena_Valida_Entrega_Usuario()
		{
			_usuarioLogic.AddUsuario(_usuario1);
			_usuarioLogic.AddUsuario(_usuario2);
			Usuario usuario = _usuarioLogic.UsuarioByCorreoContrasena("hola@gmail.com", "123456789Aaa");
			bool resultado = usuario.Equals(_usuario1);
			bool contrasenaIgual = usuario.Contrasena == _usuario1.Contrasena;
			Assert.IsTrue(resultado);
			Assert.IsTrue(contrasenaIgual);
		}

		[TestMethod]
		[ExpectedException(typeof(BussinesLogicUsuarioException))]
		public void Excepcion_Ingreso_Correo_Valido_Contrasena_No_Existente()
		{
			_usuarioLogic.AddUsuario(_usuario1);
			_usuarioLogic.AddUsuario(_usuario2);
			Usuario usuario = _usuarioLogic.UsuarioByCorreoContrasena("hola@gmail.com", "");
		}

		[TestMethod]
		[ExpectedException(typeof(BussinesLogicUsuarioException))]
		public void Excepcion_Ingreso_Correo_No_Existente()
		{
			_usuarioLogic.AddUsuario(_usuario2);
			_usuarioLogic.AddUsuario(_usuario2);
			Usuario resultado = _usuarioLogic.UsuarioByCorreoContrasena("", "");
		}

		[TestMethod]
		[ExpectedException(typeof(BussinesLogicUsuarioException))]
		public void Excepcion_Ingreso_Correo_Contrasena_Nula()
		{
			_usuarioLogic.AddUsuario(_usuario2);
			_usuarioLogic.AddUsuario(_usuario2);
			Usuario resultado = _usuarioLogic.UsuarioByCorreoContrasena(null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(BussinesLogicUsuarioException))]
		public void Excepcion_Ingreso_Correo_No_Existente_4()
		{
			_usuarioLogic.AddUsuario(_usuario1);
			_usuarioLogic.AddUsuario(_usuario2);
			Usuario resultado = _usuarioLogic.UsuarioByCorreoContrasena("123456789Aaa", null);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Nombre()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AddUsuario(usuario);

			_usuarioLogic.ModificarNombre(usuario.Correo, "Juan");

			Usuario usuarioModificado = _usuarioLogic.FindUsuario(usuario.Correo);
			Assert.AreEqual("Juan", usuarioModificado.Nombre);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Apellido()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AddUsuario(usuario);

			_usuarioLogic.ModificarApellido(usuario.Correo,"Perez");

			Usuario usuarioModificado = _usuarioLogic.FindUsuario(usuario.Correo);
			Assert.AreEqual("Perez", usuarioModificado.Apellido);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Contrasena()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AddUsuario(usuario);

			_usuarioLogic.ModificarContrasena(usuario.Correo,"123456789Aaa");

			Usuario usuarioModificado = _usuarioLogic.FindUsuario(usuario.Correo);
			Assert.AreEqual("123456789Aaa", usuarioModificado.Contrasena);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Direccion()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AddUsuario(usuario);

			_usuarioLogic.ModificarDatosUsuario(usuario.Correo, usuario.Nombre, usuario.Apellido, usuario.Contrasena, "street 56 av rety");

			Usuario usuarioModificado = _usuarioLogic.FindUsuario(usuario.Correo);
			Assert.AreEqual("street 56 av rety", usuarioModificado.Direccion);
		}
	}
}
