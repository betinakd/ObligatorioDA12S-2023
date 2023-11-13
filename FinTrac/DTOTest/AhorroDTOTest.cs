using DTO;

namespace DTOTest
{
	[TestClass]
	public class AhorroDTOTest
	{
		[TestMethod]
		public void AhorroDTO_Tiene_Monto()
		{
			AhorroDTO ahorroDTO = new AhorroDTO();
			ahorroDTO.Monto = 1000;
			Assert.AreEqual(1000, ahorroDTO.Monto);
		}

		[TestMethod]
		public void AhorroDTO_Tiene_Nombre()
		{
			AhorroDTO ahorroDTO = new AhorroDTO();
			ahorroDTO.Nombre = "Ahorro";
			Assert.AreEqual("Ahorro", ahorroDTO.Nombre);
		}

	}
}
