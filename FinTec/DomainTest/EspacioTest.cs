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
			Assert.AreEqual(espacio.Admin,usuario);
		}
	}
}
