using Domain;
namespace DomainTest
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
		[ExpectedException(typeof(DomainCuentaException))]
		public void Excepcion_CuentaAhorro_Tiene_Nombre_Vacio()
		{
			string nombre = "";
			cuentaAhorro.Nombre = nombre;
		}

		[TestMethod]
		public void Cuenta_Tiene_Monto_Decimal() { 
			double monto = 100.01;
			cuentaAhorro.Monto = monto;
			Assert.AreEqual(monto, cuentaAhorro.Monto);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCuentaException))]
		public void Excepcion_CuentaAhorro_Monto_Negativo()
		{
			double monto = -100.01;
			cuentaAhorro.Monto = monto;
		}

		[TestMethod]
		public void CuentaAhorro_Tiene_ToString()
		{
			Ahorro cuentaAhorro = new Ahorro();
			cuentaAhorro.Nombre = "CuentaAhorroPrueba";
			cuentaAhorro.Monto = 100.01;
			Cuenta cuenta = cuentaAhorro;
			Assert.AreEqual(cuenta.ToString() , cuentaAhorro.ToString());
		}

		[TestMethod]
		public void Ahorro_Equals_Null()
		{
			var credito = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Monto = 100.01,
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
				Monto = 100.01,
			};

			var credito2 = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Monto = 100.01,
			};

			Assert.IsTrue(credito.Equals(credito2));
		}

		[TestMethod]
		public void Ahorro_Equals_False_Diferente_Nombre()
		{
			var credito = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Monto = 100.01,
			};

			var credito2 = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba2",
				Monto = 100.01,
			};

			Assert.IsFalse(credito.Equals(credito2));
		}

		[TestMethod]
		public void Modificar_Cuenta_Ahorro() {
			var cuenta = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba",
				Monto = 100.01,
			};
			var modificacion = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "CuentaAhorroPrueba2",
				Monto = 100.01,
			};
			cuenta.Modificar(modificacion);
			Assert.AreEqual(cuenta.Nombre, modificacion.Nombre);
		}
	}
}
