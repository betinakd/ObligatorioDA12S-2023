using BussinesLogic;
using Controlador;
using Domain;
using Repository;
using DTO;

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
			
			UsuarioDTO usuarioDTO = controladorTest.DatosAdminEspacio(1);
			
			Assert.AreEqual("Juan", usuarioDTO.Nombre);
			Assert.AreEqual("Perez", usuarioDTO.Apellido);
			Assert.AreEqual("hola@gmail.com", usuarioDTO.Correo);
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
			List<UsuarioDTO> resultado = controladorTest.DatosUsuariosInvitadosEspacio(1);
			UsuarioDTO usuario = resultado.FirstOrDefault();
			Assert.AreEqual("Juana", usuario.Nombre);
			Assert.AreEqual("Perez", usuario.Apellido);
			Assert.AreEqual("chau@gmail.com", usuario.Correo);
			Assert.AreEqual(1, resultado.Count);
		}

		[TestMethod]
		public void ControladorUsuarios_DatosUsuariosNoPresentesEspacio()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			Usuario usuarioTest1 = new Usuario()
			{
				Correo = "test2@gmail.com",
				Nombre = "Roberto",
				Apellido = "Ramirez",
				Contrasena = "HOLAeehola123",
				Direccion = "Bv España 4444"
			};
			Usuario usuarioTest2 = new Usuario()
			{
				Correo = "test3@gmail.com",
				Nombre = "Julio",
				Apellido = "Martinez",
				Contrasena = "HOLeeehola123",
				Direccion = "Bv España 546"
			};
			_usuarioLogic.AddUsuario(usuario);
			_usuarioLogic.AddUsuario(usuarioTest1);
			_usuarioLogic.AddUsuario(usuarioTest2);
			Espacio espacio = new Espacio()
			{
				Id = 1,
				Nombre = "Principal " + usuario.Nombre,
				Admin = usuario
			};
			ControladorUsuarios controladorTest = new ControladorUsuarios(_usuarioLogic, _espacioLogic);
			_espacioLogic.AddEspacio(espacio);
			string[,] resultado = controladorTest.DatosUsuariosNoPresentesEspacio(1);
			Assert.AreEqual("Roberto", resultado[0, 0]);
			Assert.AreEqual("Ramirez", resultado[0, 1]);
			Assert.AreEqual("test2@gmail.com", resultado[0, 2]);
			Assert.AreEqual("Julio", resultado[1, 0]);
			Assert.AreEqual("Martinez", resultado[1, 1]);
			Assert.AreEqual("test3@gmail.com", resultado[1, 2]);
			Assert.AreEqual(2, resultado.GetLength(0));
			Assert.AreEqual(3, resultado.GetLength(1));
		}

		[TestMethod]
		public void ControladorUsuarios_AgregarUsuarioAEspacio()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			Usuario usuarioTest1 = new Usuario()
			{
				Correo = "test2@gmail.com",
				Nombre = "Roberto",
				Apellido = "Ramirez",
				Contrasena = "HOLAeehola123",
				Direccion = "Bv España 4444"
			};
			Usuario usuarioTest2 = new Usuario()
			{
				Correo = "test3@gmail.com",
				Nombre = "Julio",
				Apellido = "Martinez",
				Contrasena = "HOLeeehola123",
				Direccion = "Bv España 546"
			};
			_usuarioLogic.AddUsuario(usuario);
			_usuarioLogic.AddUsuario(usuarioTest1);
			_usuarioLogic.AddUsuario(usuarioTest2);
			Espacio espacio = new Espacio()
			{
				Id = 1,
				Nombre = "Principal " + usuario.Nombre,
				Admin = usuario
			};
			ControladorUsuarios controladorTest = new ControladorUsuarios(_usuarioLogic, _espacioLogic);
			_espacioLogic.AddEspacio(espacio);
			controladorTest.AgregarUsuarioAEspacio(1, "test3@gmail.com");
			controladorTest.AgregarUsuarioAEspacio(1, "test2@gmail.com");
			Assert.AreEqual(2, espacio.UsuariosInvitados.Count);
		}

		[TestMethod]
		public void ControladorUsuarios_EliminarUsuarioDeEspacio_Elimina_Usuario_Con_Correo()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			Usuario usuarioTest1 = new Usuario()
			{
				Correo = "test2@gmail.com",
				Nombre = "Roberto",
				Apellido = "Ramirez",
				Contrasena = "HOLAeehola123",
				Direccion = "Bv España 4444"
			};
			Usuario usuarioTest2 = new Usuario()
			{
				Correo = "test3@gmail.com",
				Nombre = "Julio",
				Apellido = "Martinez",
				Contrasena = "HOLeeehola123",
				Direccion = "Bv España 546"
			};
			_usuarioLogic.AddUsuario(usuario);
			_usuarioLogic.AddUsuario(usuarioTest1);
			_usuarioLogic.AddUsuario(usuarioTest2);
			Espacio espacio = new Espacio()
			{
				Id = 1,
				Nombre = "Principal " + usuario.Nombre,
				Admin = usuario
			};
			espacio.InvitarUsuario(usuarioTest1);
			espacio.InvitarUsuario(usuarioTest2);
			ControladorUsuarios controladorTest = new ControladorUsuarios(_usuarioLogic, _espacioLogic);
			_espacioLogic.AddEspacio(espacio);
			Assert.IsTrue(espacio.UsuariosInvitados.Contains(usuarioTest2));
			controladorTest.EliminarUsuarioDeEspacio(1, "test3@gmail.com");
			Assert.IsTrue(espacio.UsuariosInvitados.Contains(usuarioTest1));
			Assert.IsFalse(espacio.UsuariosInvitados.Contains(usuarioTest2));
		}
	}
}
