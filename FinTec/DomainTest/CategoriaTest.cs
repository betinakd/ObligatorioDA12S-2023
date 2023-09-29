using Domain;

namespace DomainTest
{
	[TestClass]
	public class CategoriaTest
	{
		private Categoria categoria;

		[TestInitialize]
		public void Inicializar()
		{
			categoria = new Categoria();
		}

		[TestMethod]
		public void Nueva_Categoria()
		{
			Assert.IsNotNull(categoria);
		}

		[TestMethod]
		public void Categoria_Tiene_Nombre()
		{
			string nombre = "CategoriaPrueba";
			categoria.Nombre = nombre;
			Assert.AreEqual(nombre, categoria.Nombre);
		}
	}
}
