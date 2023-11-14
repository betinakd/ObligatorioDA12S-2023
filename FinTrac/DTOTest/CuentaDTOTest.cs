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
			cuentaDTO2.Moneda = TipoCambiarioDTO.Euro;
			CuentaDTO cuentaDTO3 = new CuentaDTO();
			cuentaDTO3.Moneda = TipoCambiarioDTO.Dolar;
			Assert.AreEqual(cuentaDTO3.Moneda, TipoCambiarioDTO.Dolar);
			Assert.AreEqual(cuentaDTO2.Moneda, TipoCambiarioDTO.Euro);
			Assert.AreEqual(cuentaDTO.Moneda, TipoCambiarioDTO.PesosUruguayos);
		}

		[TestMethod]
		public void CuentaDTO_Tiene_FechaCreacion()
		{
			CuentaDTO cuentaDTO = new CuentaDTO();
			DateTime dateTime = new DateTime(2019, 1, 1);
			cuentaDTO.FechaCreacion = dateTime;
			Assert.AreEqual(cuentaDTO.FechaCreacion, dateTime);
		}

		[TestMethod]
		public void CuentaDTO_Tiene_Id()
		{
			CuentaDTO cuentaDTO = new CuentaDTO();
			cuentaDTO.Id = 1;
			Assert.AreEqual(cuentaDTO.Id, 1);
		}

		[TestMethod]
		public void CuentaDTO_Tiene_Saldo()
		{ 
			CuentaDTO cuentaDTO = new CuentaDTO();
			cuentaDTO.Saldo = 1000;
			Assert.AreEqual(cuentaDTO.Saldo, 1000);
		}
	}
}
