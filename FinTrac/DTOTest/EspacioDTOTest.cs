using DTO;
namespace DTOTest
{
	[TestClass]
	public class EspacioDTOTest
	{
		[TestMethod]
		public void EspacioDTO_Tiene_Nombre()
		{
			EspacioDTO espacio = new EspacioDTO();
			string nombre = "EspacioTest";
			espacio.Nombre = nombre;
			Assert.AreEqual(nombre, espacio.Nombre);
		}
	}
}
