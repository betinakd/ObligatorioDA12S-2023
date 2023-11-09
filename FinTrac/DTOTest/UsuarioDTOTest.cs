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
	}
}