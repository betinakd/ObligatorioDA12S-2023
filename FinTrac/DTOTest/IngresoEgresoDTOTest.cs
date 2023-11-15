using DTO;
using DTO.EnumsDTO;
namespace DTOTest
{
	[TestClass]
	public class IngresoEgresoDTOTest
	{
		[TestMethod]
		public void IngresoEgresoDTO_Tiene_Fecha()
		{
			IngresoEgresoDTO ingresoEgreso = new IngresoEgresoDTO();
			ingresoEgreso.Fecha = DateTime.Today;
			Assert.AreEqual(DateTime.Today, ingresoEgreso.Fecha);
		}
	}
}
