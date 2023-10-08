using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainTest
{
	[TestClass]
	public class ObjetivoTest
	{
		private Categoria _categoria;
		private List<Categoria> _categorias = new List<Categoria>();
		private Objetivo _objetivo;

		[TestInitialize]
		public void InitTest()
		{
			_categoria = new Categoria()
			{
				Nombre = "Sueldo",
				Tipo = TipoCategoria.Ingreso,
				EstadoActivo = true
			};
			_categorias.Add(_categoria);

			_objetivo = new Objetivo()
			{
				Titulo = "Objetivo 1",
				MontoMaximo = 1000,
				Categorias = _categorias,
			};
		}

		[TestMethod]
		public void Nuevo_Objetivo()

		{
			var objetivo = new Objetivo();
			Assert.IsNotNull(objetivo);
		}

		[TestMethod]
		public void Tiene_Titulo()
		{
			_objetivo.Titulo = "Objetivo 1";
			Assert.AreEqual("Objetivo 1", _objetivo.Titulo);
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void Tiene_Titulo_Vacio()

		{
			_objetivo.Titulo = "";
		}

		[TestMethod]
		public void Monto_Maximo()

		{
			_objetivo.MontoMaximo = 1000;
			Assert.AreEqual(1000, _objetivo.MontoMaximo);
		}

		[TestMethod]
		public void Aplicar_Ojetivo_Categoria()

		{
			var objetivo = new Objetivo();
			var categoria = new Categoria();
			var categorias = new List<Categoria>();
			categoria.EstadoActivo = true;
			categoria.Tipo = TipoCategoria.Ingreso;
			categoria.Nombre = "Sueldo";
			categorias.Add(categoria);
			objetivo.Categorias = categorias;
			var resultado = objetivo.Categorias;
			Assert.AreEqual(categorias, resultado);
		}

	
	}
}
