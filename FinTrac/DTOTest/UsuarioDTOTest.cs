using DTO;
namespace DTOTest
{
	[TestClass]
	public class UsuarioDTOTest
	{
		[TestMethod]
		public void UsuarioDTO_Tiene_Nombre()
		{
			UsuarioDTO usuario = new UsuarioDTO();
			usuario.Nombre = "Juan";
			Assert.AreEqual("Juan", usuario.Nombre);
		}

		[TestMethod]
		public void UsuarioDTO_Tiene_Apellido()
		{
			UsuarioDTO usuario = new UsuarioDTO();
			usuario.Apellido = "Lopez";
			Assert.AreEqual("Lopez", usuario.Apellido);
		}

		[TestMethod]
		public void UsuarioDTO_Tiene_Correo()
		{
			UsuarioDTO usuario = new UsuarioDTO();
			usuario.Correo = "Lopez@gmail.com";
			Assert.AreEqual("Lopez@gmail.com", usuario.Correo);
		}

		[TestMethod]
		public void UsuarioDTO_Tiene_Contrasena()
		{
			UsuarioDTO usuario = new UsuarioDTO();
			string contrasena = "TestTest123";
			usuario.Contrasena = contrasena;
			Assert.AreEqual(contrasena, usuario.Contrasena);
		}

		[TestMethod]
		public void UsuarioDTO_Tiene_Direccion()
		{
			UsuarioDTO usuario = new UsuarioDTO();
			string direccion = "Av españa 4567";
			usuario.Direccion = direccion;
			Assert.AreEqual(direccion, usuario.Direccion);
		}

		[TestMethod]
		public void UsuarioDTO_Tiene_IdEspacioPrincipal()
		{
			UsuarioDTO usuario = new UsuarioDTO();
			int idEspacioPrincipal = 1;
			usuario.IdEspacioPrincipal = idEspacioPrincipal;
			Assert.AreEqual(idEspacioPrincipal, usuario.IdEspacioPrincipal);
		}
	}
}