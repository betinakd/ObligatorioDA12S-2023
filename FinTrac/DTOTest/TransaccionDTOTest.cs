using DTO;
using DTO.EnumsDTO;

namespace DTOTest
{
	[TestClass]
	public class TransaccionDTOTest
	{
		[TestMethod]
		public void TransaccionDTO_Tiene_Id()
		{
			TransaccionDTO transaccionDTO = new TransaccionDTO();
			transaccionDTO.Id = 1;
			Assert.AreEqual(1, transaccionDTO.Id);
		}

		[TestMethod]
		public void TransaccionDTO_Tiene_Titulo()
		{
			TransaccionDTO transaccionDTO = new TransaccionDTO();
			transaccionDTO.Titulo = "Titulo";
			Assert.AreEqual("Titulo", transaccionDTO.Titulo);
		}
	}
}
