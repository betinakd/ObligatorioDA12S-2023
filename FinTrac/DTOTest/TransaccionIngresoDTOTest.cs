using DTO;
using DTO.EnumsDTO;
namespace DTOTest
{
	[TestClass]
	public class TransaccionIngresoDTOTest
	{
		[TestMethod]
		public void TransaccionIngres_No_Es_Null()
		{
			TransaccionDTO transaccionDTO = new TransaccionIngresoDTO();
			Assert.IsNotNull(transaccionDTO);
		}
	}
}
