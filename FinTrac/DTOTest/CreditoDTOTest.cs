using DTO;

namespace DTOTest
{
	[TestClass]
	public class CreditoDTOTest
	{
		[TestMethod]
		public void CreditoDTO_Tiene_BancoEmisor()
		{
			var creditoDTO = new CreditoDTO();
			creditoDTO.BancoEmisor = "Banco de Chile";
			Assert.AreEqual("Banco de Chile", creditoDTO.BancoEmisor);
		}

		[TestMethod]
		public void CreditoDTO_Tiene_NumeroTarjeta()
		{
			var creditoDTO = new CreditoDTO();
			creditoDTO.NumeroTarjeta = "1234";
			Assert.AreEqual("1234", creditoDTO.NumeroTarjeta);
		}

		[TestMethod]
		public void CreditoDTO_Tiene_CreditoDisponible()
		{
			var creditoDTO = new CreditoDTO();
			creditoDTO.CreditoDisponible = 1000;
			Assert.AreEqual(1000, creditoDTO.CreditoDisponible);
		}

		[TestMethod]
		public void CreditoDTO_Tiene_FechaCierre()
		{
			var creditoDTO = new CreditoDTO();
			DateTime fecha = DateTime.Now;
			creditoDTO.FechaCierre = fecha;
			Assert.AreEqual(fecha, creditoDTO.FechaCierre);
		}
	}
}