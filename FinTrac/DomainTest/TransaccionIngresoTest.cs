using Domain;
using Excepcion;

namespace DomainTest
{
	[TestClass]
	public class TransaccionIngresoTest
	{
		private TransaccionIngreso transaccion1;
		private TransaccionIngreso transaccion2;
		private Ahorro ahorro1;
		private Credito credito1;

		[TestInitialize]
		public void InitTests()
		{
			var ahorro1 = new Ahorro()
			{
				Nombre = "Ahorro1",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
			};
			credito1 = new Credito()
			{
				NumeroTarjeta = "4444",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
				FechaCierre = DateTime.Today.AddDays(4),
				BancoEmisor = "Banco1",
			};
			transaccion1 = new TransaccionIngreso()
			{
				Monto = 100,
				Titulo = "Transaccion1",
				Moneda = TipoCambiario.Dolar,
				CuentaMonetaria = credito1,
				FechaTransaccion = DateTime.Today,
			};
			transaccion2 = new TransaccionIngreso()
			{
				Monto = 1000,
				Titulo = "Transaccion2",
				Moneda = TipoCambiario.Dolar,
				CuentaMonetaria = ahorro1,
				FechaTransaccion = DateTime.Today,
			};
		}

		[TestMethod]
		public void Categoria_Transaccion()
		{
			var transaccion = new TransaccionIngreso();
			Categoria categoria = new Categoria();
			categoria.Nombre = "Categoria1";
			categoria.Tipo = TipoCategoria.Ingreso;
			categoria.EstadoActivo = true;
			transaccion.CategoriaTransaccion = categoria;
			Assert.AreEqual(categoria, transaccion.CategoriaTransaccion);
		}


		[TestMethod]
		public void TransaccionIngreso_Tiene_Cuenta_Monetaria_Valida()
		{
			transaccion1.CuentaMonetaria = new Ahorro()
			{
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
			};
		}

		[TestMethod]
		public void TransaccionIngreso_Tiene_Tipo() {
			TransaccionIngreso transIngreso = new TransaccionIngreso()
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
				CuentaMonetaria = new Ahorro()
				{
					Nombre = "Cuenta1",
					Moneda = TipoCambiario.Dolar,
					Saldo = 100,
				},
			};
			string tieneTipo = transIngreso.Tipo();
			Assert.AreEqual("Ingreso", tieneTipo);
		}

		[TestMethod]
		public void TransaccionIngreso_EjecutaTransaccion()
		{
			Cuenta cuenta = new Ahorro()
			{
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
			};

			Transaccion transIngreso = new TransaccionIngreso()
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

			transIngreso.EjecutarTransaccion();
			Assert.AreEqual(200, cuenta.Saldo);
		}
	}
}
