using DTO.EnumsDTO;
using DTO;

namespace DTOTest
{
	[TestClass]
	public class CuentaDTOTest
	{
		[TestMethod]
		public void CuentaDTO_Tiene_Moneda()
		{
			CuentaDTO cuentaDTO = new CuentaDTO();
			cuentaDTO.Moneda = TipoCambiarioDTO.PesosUruguayos;
			CuentaDTO cuentaDTO2 = new CuentaDTO();
			cuentaDTO.Moneda = TipoCambiarioDTO.Euro;
			CuentaDTO cuentaDTO3 = new CuentaDTO();
			cuentaDTO.Moneda = TipoCambiarioDTO.Dolar;
			Assert.AreEqual(cuentaDTO.Moneda, TipoCambiarioDTO.Dolar);
			Assert.AreEqual(cuentaDTO.Moneda, TipoCambiarioDTO.Euro);
			Assert.AreEqual(cuentaDTO.Moneda, TipoCambiarioDTO.PesosUruguayos);
		}
	}
}
