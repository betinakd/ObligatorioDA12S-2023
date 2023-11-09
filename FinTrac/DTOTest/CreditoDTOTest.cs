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
	}
}