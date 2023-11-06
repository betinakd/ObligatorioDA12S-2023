using BussinesLogic;
using Controlador;
using Domain;
using Repository;

namespace ControladorTest
{
	[TestClass]
	public class ControladorUsuariosTest
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
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public void ControladorUsuarios_Tiene_UsuarioLogic()
		{
			ControladorUsuarios controladorTest = new ControladorUsuarios(_usuarioLogic,_espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorUsuarios_Tiene_EspacioLogic()
		{
			ControladorUsuarios controladorTest = new ControladorUsuarios(_usuarioLogic,_espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorUsuarios_DatosAdminEspacio()
		{			
			Usuario admin = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			Espacio espacio = new Espacio()
			{
				Id = 1,
				Nombre = "Principal " + admin.Nombre,
				Admin = admin
			};
			_usuarioLogic.AddUsuario(admin);
			_espacioLogic.AddEspacio(espacio);
			ControladorUsuarios controladorTest = new ControladorUsuarios(_usuarioLogic, _espacioLogic);
			
			string[] datos = controladorTest.DatosAdminEspacio(1);
			
			Assert.AreEqual("Juan", datos[0]);
			Assert.AreEqual("Perez", datos[1]);
			Assert.AreEqual("hola@gmail.com", datos[2]);
		}

		[TestMethod]
		public void ControladorUsuarios_DatosUsuariosInvitadosEspacio()
		{
			Usuario invitado1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			Usuario invitado2 = new Usuario()
			{
				Correo = "chau@gmail.com",
				Nombre = "Juana",
				Apellido = "Perez",
				Contrasena = "123456789Aawwa",
				Direccion = "av bvar artigas 56"
			};
			Espacio espacio = new Espacio()
			{
				Id = 1,
				Nombre = "Principal " + invitado1.Nombre,
				Admin = invitado1
			};
			espacio.InvitarUsuario(invitado2);
			_usuarioLogic.AddUsuario(invitado1);
			_usuarioLogic.AddUsuario(invitado2);
			_espacioLogic.AddEspacio(espacio);
			ControladorUsuarios controladorTest = new ControladorUsuarios(_usuarioLogic, _espacioLogic);
			string[,] resultado = controladorTest.DatosUsuariosInvitadosEspacio(1);
			Assert.AreEqual("Juana", resultado[0, 0]);
			Assert.AreEqual("Perez", resultado[0, 1]);
			Assert.AreEqual("chau@gmail.com", resultado[0, 2]);
			Assert.AreEqual(1, resultado.GetLength(0));
			Assert.AreEqual(3, resultado.GetLength(1));
		}
	}
}
