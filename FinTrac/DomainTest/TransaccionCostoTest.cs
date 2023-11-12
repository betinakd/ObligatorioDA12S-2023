using Domain;
using Excepcion;

namespace DomainTest
{
	[TestClass]
	public class TransaccionCostoTest
	{
		private TransaccionCosto transaccion1;
		private TransaccionCosto transaccion2;
		private Ahorro cuenta1;

		[TestInitialize]
		public void InitTests()
		{
			cuenta1 = new Ahorro()
			{
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.Dolar,
				Monto = 100,
			};
			var cuenta2 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "BROU",
				CreditoDisponible = 100,
				Moneda = TipoCambiario.Dolar,
				FechaCierre = DateTime.Now.AddDays(4),
			};
			transaccion1 = new TransaccionCosto()
			{
				Monto = 100,
				Titulo = "Transaccion1",
				Moneda = TipoCambiario.Dolar,
				CuentaMonetaria = cuenta1,
				FechaTransaccion = DateTime.Today,
			};
			transaccion2 = new TransaccionCosto()
			{
				Monto = 1090,
				Titulo = "Transaccion1",
				Moneda = TipoCambiario.Dolar,
				CuentaMonetaria = cuenta2,
				FechaTransaccion = DateTime.Today,
			};
		}

		[TestMethod]
		public void Categoria_Transaccion()
		{
			var transaccion = new TransaccionCosto();
			Categoria categoria = new Categoria();
			categoria.Nombre = "Categoria1";
			categoria.Tipo = TipoCategoria.Costo;
			categoria.EstadoActivo = true;
			transaccion.CategoriaTransaccion = categoria;
			Assert.AreEqual(categoria, transaccion.CategoriaTransaccion);
		}

		[TestMethod]
		public void TransaccionCosto_Clon_Cuenta_Ahorro()
		{
			var transaccionClon = transaccion1.ClonTransaccion();
			Assert.AreEqual(transaccion1.Titulo, transaccionClon.Titulo);
			Assert.AreEqual(transaccion1.Moneda, transaccionClon.Moneda);
			Assert.AreEqual(transaccion1.CuentaMonetaria, transaccionClon.CuentaMonetaria);
			Assert.AreEqual(transaccion1.CategoriaTransaccion, transaccionClon.CategoriaTransaccion);
			Assert.AreEqual(transaccion1.Monto, transaccionClon.Monto);
		}

		[TestMethod]
		public void TransaccionCosto_Clon_Cuenta_Credito()
		{
			var transaccionClon = transaccion2.ClonTransaccion();
			Assert.AreEqual(transaccion2.Titulo, transaccionClon.Titulo);
			Assert.AreEqual(transaccion2.Moneda, transaccionClon.Moneda);
			Assert.AreEqual(transaccion2.CuentaMonetaria, transaccionClon.CuentaMonetaria);
			Assert.AreEqual(transaccion2.CategoriaTransaccion, transaccionClon.CategoriaTransaccion);
			Assert.AreEqual(transaccion2.Monto, transaccionClon.Monto);
		}

		[TestMethod]
		public void TransaccionCosto_Tiene_Cuenta_Monetaria_Valida()
		{
			transaccion1.CuentaMonetaria = new Ahorro()
			{
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.Dolar,
				Monto = 100,
			};
		}

		[TestMethod]
		public void TransaccionCosto_Tiene_Tipo()
		{
			TransaccionCosto transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = cuenta1,
				FechaTransaccion = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Categoria1",
					Tipo = TipoCategoria.Costo,
					EstadoActivo = true,
				},
				Monto = 100,
				Titulo = "Transaccion1",
			};
			string tipo = transaccion.Tipo();
			Assert.AreEqual("Costo", tipo);
		}
	}
}

