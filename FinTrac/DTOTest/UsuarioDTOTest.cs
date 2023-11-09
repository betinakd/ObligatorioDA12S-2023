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
	}
}