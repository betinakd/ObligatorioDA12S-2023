using Domain;
using DTO;
using DTO.EnumsDTO;

namespace DTOTest
{
	[TestClass]
	public class CategoriaGastoDTOTest
	{
		[TestMethod]
		public void CategoriaGastoDTO_Tiene_Categoria()
		{
			CategoriaDTO categoria = new CategoriaDTO()
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Id = 1,
				Nombre = "PruebaCat",
				Tipo = TipoCategoriaDTO.Costo,
			};
			CategoriaGastoDTO categoriaGasto = new CategoriaGastoDTO();
			categoriaGasto.Categoria = categoria;
			Assert.IsTrue(categoriaGasto.Categoria.Equals(categoria));
		}

		[TestMethod]
		public void CategoriaGastoDTO_Tiene_Monto()
		{
			CategoriaGastoDTO categoriaGasto = new CategoriaGastoDTO();
			double monto = 10;
			categoriaGasto.MontoUsado = monto;
			Assert.IsTrue(categoriaGasto.MontoUsado == monto);
		}

		[TestMethod]
		public void CategoriaGastoDTO_Tiene_Porcentaje()
		{
			CategoriaGastoDTO categoriaGasto = new CategoriaGastoDTO();
			double porc = 10;
			categoriaGasto.Porcentaje = porc;
			Assert.IsTrue(categoriaGasto.Porcentaje == porc);
		}
	}
}
