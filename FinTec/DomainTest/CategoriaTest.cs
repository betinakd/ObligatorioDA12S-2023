using Domain;

namespace DomainTest
{
	[TestClass]
	public class CategoriaTest
	{
		[TestMethod]
		public void Nueva_Categoria()
		{
			var categoria = new Categoria();
			Assert.IsNotNull(categoria);
		}
	}
}
