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
				Saldo = 100,
			};
			var cuenta2 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "BROU",
				Saldo = 100,
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
		public void TransaccionCosto_Tiene_Cuenta_Monetaria_Valida()
		{
			transaccion1.CuentaMonetaria = new Ahorro()
			{
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
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

		[TestMethod]
		public void TransaccionCosto_EjecutaTransaccion()
		{
			Cuenta cuenta = new Ahorro()
			{
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
			};

			Transaccion transCosto = new TransaccionCosto()
			{
				FechaTransaccion = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Monto = 100,
				Titulo = "Transaccion1",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Categoria1",
					Tipo = TipoCategoria.Ingreso,
					EstadoActivo = true,
				},
				CuentaMonetaria = cuenta,
			};

			transCosto.EjecutarTransaccion();
			Assert.AreEqual(0, cuenta.Saldo);
		}

		[TestMethod]
		public void Transaccion_Costo_Modifica_Monto_Y_Cambiar_Valor_Cuenta_Correctamente()
		{
			Cuenta cuenta = new Ahorro()
			{
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
			};

			Transaccion transCosto = new TransaccionCosto()
			{
				FechaTransaccion = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Monto = 100,
				Titulo = "Transaccion1",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Categoria1",
					Tipo = TipoCategoria.Ingreso,
					EstadoActivo = true,
				},
				CuentaMonetaria = cuenta,
			};

			transCosto.ModificarMonto(200);

			Assert.AreEqual(200, transCosto.Monto);
			Assert.AreEqual(0, cuenta.Saldo);

			transCosto.ModificarMonto(1);

			Assert.AreEqual(1, transCosto.Monto);
			Assert.AreEqual(199, cuenta.Saldo);
		}
	}
}

