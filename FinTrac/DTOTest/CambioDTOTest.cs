using DTO;
using DTO.EnumsDTO;

namespace DTOTest
{
	[TestClass]
	public class CambioDTOTest
	{
		[TestMethod]
		public void CambioDTO_Tiene_Moneda()
		{ 
			CambioDTO cambio = new CambioDTO();
			cambio.Moneda = TipoCambiarioDTO.Dolar;
			CambioDTO cambio2 = new CambioDTO();
			cambio2.Moneda = TipoCambiarioDTO.Euro;
			CambioDTO cambio3 = new CambioDTO();
			cambio3.Moneda = TipoCambiarioDTO.PesosUruguayos;
			Assert.AreEqual(TipoCambiarioDTO.PesosUruguayos, cambio3.Moneda);
			Assert.AreEqual(TipoCambiarioDTO.Euro, cambio2.Moneda);
			Assert.AreEqual(TipoCambiarioDTO.Dolar, cambio.Moneda);
		}
	}
}
