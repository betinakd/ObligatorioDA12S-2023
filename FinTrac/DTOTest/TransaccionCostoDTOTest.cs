using DTO;
using DTO.EnumsDTO;

namespace DTOTest
{
	[TestClass]
	public class TransaccionCostoDTOTest
	{
		[TestMethod]
		public void TransaccionCosto_No_Es_Null()
		{
			TransaccionDTO transaccionDTO = new TransaccionCostoDTO();
			Assert.IsNotNull(transaccionDTO);
		}
	}
}
