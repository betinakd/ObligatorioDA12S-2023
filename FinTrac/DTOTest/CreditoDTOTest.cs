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
	}
}