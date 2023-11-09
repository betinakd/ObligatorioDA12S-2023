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
	}
}