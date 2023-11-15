using Controlador;
using DTO;
using Domain;
using EspacioReporte;
using DTO.EnumsDTO;
using BussinesLogic;
using Repository;

namespace ControladorTest
{

	

	[TestClass]
	public class ControladorReporteTest
	{
		private IRepository<Espacio> _repositorioEspacio;
		private EspacioLogic _espacioLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			_espacioLogic = new EspacioLogic(_repositorioEspacio);
		}

		[TestMethod]
		public void ControladorReporte_Se_Crea()
		{
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			Assert.IsNotNull(controladorReporte);
		}


		[TestMethod]
		public void ReporteObjetivosGasto_Existe()
		{
			Espacio espacio = new Espacio() { Id = 1 };
			Categoria categoria = new Categoria()
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Id = 1,
				Nombre = "PruebaCat",
				Tipo = TipoCategoria.Costo,
			};
			espacio.AgregarCategoria(categoria);
			List<Categoria> listaCategorias = new List<Categoria>();
			listaCategorias.Add(categoria);
			Objetivo objetivo = new Objetivo()
			{
				Categorias = listaCategorias,
				Id = 1,
				MontoMaximo = 1000,
				Titulo = "ObjetivoPrueba",
				Token = "token",
			};
			espacio.AgregarObjetivo(objetivo);
			Transaccion transaccion = new Transaccion()
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Monto = 100,
				CategoriaTransaccion = categoria,
			};
			espacio.AgregarTransaccion(transaccion);
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<ObjetivoGastoDTO> reporteControlador = controladorReporte.ReporteObjetivosGastos(1);
			Assert.IsTrue(reporteControlador.Count == 1);
		}
	}
}
