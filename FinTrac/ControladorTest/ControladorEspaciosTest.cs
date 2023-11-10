using BussinesLogic;
using Controlador;
using Domain;
using Excepcion;
using Repository;
using DTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorEspaciosTest
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
		public void ControladorEspacios_Tiene_UsuarioLogic()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorEspacios_Tiene_EspacioLogic()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void CrearEspacio_Lanza_Excepcion_Crear_Espacio_CorreoAdmin_No_Existe()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			string errorMensaje = controladorTest.CrearEspacio("test@gmail.com", "EspacioTest");
			Assert.AreEqual("El usuario no existe", errorMensaje);
		}

		[TestMethod]
		public void CrearEspacio_Crea_Espacio_Correctamente_Con_Admin_Existente()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Usuario creadorEspacio = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(creadorEspacio);
			controladorTest.CrearEspacio("test@gmail.com", "Espacio Test");
			Espacio espacioCreado = _espacioLogic.FindEspacio(1);
			Assert.AreEqual("Espacio Test", espacioCreado.Nombre);
			Assert.AreEqual(creadorEspacio, espacioCreado.Admin);
		}

		[TestMethod]
		public void ModificarNombreEspacio_Modifica_Nombre_Espacio_Correctamente()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Usuario creadorEspacio = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(creadorEspacio);
			Espacio espacioCreado = new Espacio()
			{
				Id = 1,
				Nombre = "Espacio Test",
				Admin = creadorEspacio
			};
			_espacioLogic.AddEspacio(espacioCreado);
			string mensaje = controladorTest.ModificarNombreEspacio(1, "Espacio Modificado");
			Assert.AreEqual("Espacio Espacio Modificado Modificado con éxito.", mensaje);
			Assert.AreEqual("Espacio Modificado", espacioCreado.Nombre);
		}

		[TestMethod]
		public void ModificarNombreEspacio_Lanza_Mensaje_De_Excepcion_Nombre_Vacio()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Usuario creadorEspacio = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(creadorEspacio);
			Espacio espacioCreado = new Espacio()
			{
				Id = 1,
				Nombre = "test",
				Admin = creadorEspacio
			};
			_espacioLogic.AddEspacio(espacioCreado);
			string mensaje = controladorTest.ModificarNombreEspacio(1, "");
			Assert.AreEqual("El espacio debe tener un nombre", mensaje);
		}

		[TestMethod]
		public void EspaciosDeUsuario_Retorna_Espacios_Correctamente()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Usuario creadorEspacio1 = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
			Usuario creadorEspacio2 = new Usuario()
			{
				Correo = "leticia@gmail.com",
				Nombre = "Leticia",
				Apellido = "Lopez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
			_usuarioLogic.AddUsuario(creadorEspacio1);
			_usuarioLogic.AddUsuario(creadorEspacio2);
			Espacio espacioCreado1 = new Espacio()
			{
				Id = 1,
				Nombre = "Espacio Test",
				Admin = creadorEspacio1
			};
			Espacio espacioCreado2 = new Espacio()
			{
				Id = 2,
				Nombre = "Espacio Test2",
				Admin = creadorEspacio2
			};
			espacioCreado2.InvitarUsuario(creadorEspacio1);
			_espacioLogic.AddEspacio(espacioCreado1);
			_espacioLogic.AddEspacio(espacioCreado2);
			List<EspacioDTO> espacios = controladorTest.EspaciosDeUsuario("alberto@gmail.com");
			Assert.AreEqual("Espacio Test", espacios[0].Nombre);
			Assert.AreEqual(1, espacios[0].Id);
			Assert.AreEqual("Espacio Test2", espacios[1].Nombre);
			Assert.AreEqual(2 ,espacios[1].Id);
		}

	}
}
