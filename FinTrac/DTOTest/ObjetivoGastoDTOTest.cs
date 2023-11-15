using DTO;
using DTO.EnumsDTO;

namespace DTOTest
{
	[TestClass]
	public class ObjetivoGastoDTOTest
	{
		[TestMethod]
		public void ObjetivoGastoDTO_Tiene_Objetivo()
		{
			List<CategoriaDTO> lista = new List<CategoriaDTO>();
			CategoriaDTO categoria = new CategoriaDTO()
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Id = 1,
				Nombre = "PruebaCat",
				Tipo = TipoCategoriaDTO.Costo,
			};
			lista.Add(categoria);
			ObjetivoDTO objetivo = new ObjetivoDTO()
			{
				Categorias = lista,
				Id = 1,
				MontoMaximo = 1000,
				Titulo = "ObjetivoPrueba",
				Token = "token",
			};
			ObjetivoGastoDTO objetivoGasto = new ObjetivoGastoDTO();
			objetivoGasto.Objetivo = objetivo;
			Assert.IsTrue(objetivoGasto.Objetivo.Equals(objetivo));
		}

		[TestMethod]
		public void ObjetivoGastoDTO_Tiene_MontoEsperado()
		{
			ObjetivoGastoDTO objetivoGasto = new ObjetivoGastoDTO();
			double montoEsperado = 1000;
			objetivoGasto.MontoEsperado = montoEsperado;
			Assert.IsFalse(montoEsperado == objetivoGasto.MontoEsperado);
		}
	}
}
