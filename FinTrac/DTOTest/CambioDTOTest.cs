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

		[TestMethod]
		public void CambioDTO_Tiene_FechaDeCambio()
		{
			CambioDTO cambio = new CambioDTO();
			cambio.FechaDeCambio = new DateTime(2019, 10, 10);
			CambioDTO cambio2 = new CambioDTO();
			cambio2.FechaDeCambio = new DateTime(2019, 10, 11);
			CambioDTO cambio3 = new CambioDTO();
			cambio3.FechaDeCambio = new DateTime(2019, 10, 12);
			Assert.AreEqual(new DateTime(2019, 10, 12), cambio3.FechaDeCambio);
			Assert.AreEqual(new DateTime(2019, 10, 11), cambio2.FechaDeCambio);
			Assert.AreEqual(new DateTime(2019, 10, 10), cambio.FechaDeCambio);
		}

		[TestMethod]
		public void CambioDTO_Tiene_Pesos()
		{ 
			CambioDTO cambio = new CambioDTO();
			cambio.Pesos = 100;
			CambioDTO cambio2 = new CambioDTO();
			cambio2.Pesos = 200;
			CambioDTO cambio3 = new CambioDTO();
			cambio3.Pesos = 300;
			Assert.AreEqual(300, cambio3.Pesos);
			Assert.AreEqual(200, cambio2.Pesos);
			Assert.AreEqual(100, cambio.Pesos);
		}
	}
}
