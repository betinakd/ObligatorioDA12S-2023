using Dominio;
using Excepcion;

namespace DominioTest
{
	[TestClass]
	public class AhorroTest
	{
		private Ahorro cuentaAhorro;

		[TestInitialize]
		public void Inicializar()
		{
			cuentaAhorro = new Ahorro();
		}

		[TestMethod]
		public void Nueva_Cuenta_Ahorro_No_Nula()
		{
			Assert.IsNotNull(cuentaAhorro);
		}

		[TestMethod]
		public void CuentaAhorro_Tiene_Nombre()
		{
			string nombre = "CuentaAhorroPrueba";
			cuentaAhorro.Nombre = nombre;
			Assert.AreEqual(nombre, cuentaAhorro.Nombre);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioEspacioExcepcion))]
		public void Excepcion_CuentaAhorro_Tiene_Nombre_Vacio()
		{
			string nombre = "";
			cuentaAhorro.Nombre = nombre;
		}

		[TestMethod]
		public void Cuenta_Tiene_Monto_Decimal()
		{
			double monto = 100.01;
			cuentaAhorro.Saldo = monto;
			Assert.AreEqual(monto, cuentaAhorro.Saldo);
		}


		[TestMethod]
		public void CuentaAhorro_Tiene_ToString()
		{
			Ahorro cuentaAhorro = new Ahorro();
			cuentaAhorro.Nombre = "CuentaAhorroPrueba";
			cuentaAhorro.Saldo = 100.01;
			Cuenta cuenta = cuentaAhorro;
			Assert.AreEqual(cuenta.ToString(), cuentaAhorro.ToString());
		}

		[TestMethod]
		public void Ahorro_Equals_Null()
		{
			var credito = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01,
			};

			Object objeto = null;
			var objeto2 = new Object();

			Assert.IsFalse(credito.Equals(objeto));
			Assert.IsFalse(credito.Equals(objeto2));
			Assert.IsTrue(credito.Equals(credito));
		}

		[TestMethod]
		public void Ahorro_Equals_True_Igual_Nombre()
		{
			var credito = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01,
			};

			var credito2 = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01,
			};

			Assert.IsTrue(credito.Equals(credito2));
		}

		[TestMethod]
		public void Egreso_Monetario_Valido()
		{
			Ahorro cuenta = new Ahorro()
			{
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01
			};
			cuenta.EgresoMonetario(100.01);
			Assert.AreEqual(cuenta.Saldo, 0);
		}
		[TestMethod]
		public void Ingreso_Monetario_Valido()
		{
			Ahorro cuenta = new Ahorro()
			{
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01
			};
			cuenta.IngresoMonetario(100.01);
			Assert.AreEqual(cuenta.Saldo, 200.02);
		}

	[TestMethod]
		public void Ahorro_Equals_False_Diferente_Nombre()
		{
			var credito = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01,
			};

			var credito2 = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba2",
				Saldo = 100.01,
			};

			Assert.IsFalse(credito.Equals(credito2));
		}

		[TestMethod]
		public void Modificar_Cuenta_Ahorro()
		{
			var cuenta = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01,
			};
			var modificacion = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba2",
				Saldo = 100.01,
			};
			cuenta.Modificar(modificacion);
			Assert.AreEqual(cuenta.Nombre, modificacion.Nombre);
		}

		[TestMethod]
		public void CuentaAhorro_Tiene_TipoCuenta()
		{
			var cuenta = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Saldo = 100.01,
			};
			Assert.AreEqual(cuenta.TipoDeCuenta(), TipoCuenta.EsAhorro);
		}
	}
}
