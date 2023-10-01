using Domain;
namespace DomainTest
{
	[TestClass]
	public class EspacioTest
	{
		[TestMethod]
		public void Nueva_Espacio_No_Nulo()
		{
			var espacio = new Espacio();
			Assert.IsNotNull(espacio);
		}


		[TestMethod]
		public void Espacio_Tiene_Admin()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			espacio.Admin = usuario;
			Assert.AreEqual(espacio.Admin, usuario);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Espacio_Tiene_Admin_Nulo()
		{
			var espacio = new Espacio();
			espacio.Admin = null;
		}

		[TestMethod]
		public void Espacio_Tiene_UsuariosInvitados()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();
			espacio.Admin = usuario;
			espacio.UsuariosInvitados = usuarios;
			Assert.AreEqual(espacio.UsuariosInvitados, usuarios);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Espacio_Admin_En_UsuariosInvitados()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();
			espacio.Admin = usuario;
			usuarios.Add(usuario);
			espacio.UsuariosInvitados = usuarios;
		}

		[TestMethod]
		public void Cambiar_Admin_Y_Verificar_ListaDeUsuarios()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			Usuario usuario2 = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();

			// Creamos lista de usuarios invitados con el usuario2 incluido
			usuarios.Add(usuario2);
			espacio.UsuariosInvitados = usuarios;
			// Hacemos admin al usuario
			espacio.Admin = usuario;
			// Cambiamos de admin a usuario2 por lo que usuario deberia estar en la lista de invitados y usuario2 no
			espacio.cambiarAdmin(usuario2);
			Assert.AreEqual(espacio.Admin, usuario2);
			CollectionAssert.DoesNotContain(espacio.UsuariosInvitados, usuario2);
			CollectionAssert.Contains(espacio.UsuariosInvitados, usuario);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_CambiarAdmin_No_Presente_Usuarios_Invitados()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			Usuario usuario2 = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();

			// Creamos lista de usuarios invitados con el usuario2 incluido
			espacio.UsuariosInvitados = usuarios;
			// Hacemos admin al usuario
			espacio.Admin = usuario;
			// Cambiamos de admin a usuario2 por lo que usuario deberia estar en la lista de invitados y usuario2 no
			espacio.cambiarAdmin(usuario2);
		}

		[TestMethod]
		public void Espacio_Invitar_Usuario() 
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			Usuario usuario2 = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();
			usuarios.Add(usuario);
			espacio.Admin = usuario2;
			espacio.UsuariosInvitados = usuarios;
			espacio.invitarUsuario(usuario);
		}
	}
}
