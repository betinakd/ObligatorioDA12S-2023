using Domain;
namespace DomainTest
{
	[TestClass]
	public class CreditoTest
	{
		[TestMethod]
		public void Nueva_Cuenta_Credito_No_Nula()
		{
			Credito cuentaCredito = new Credito();
			Assert.IsNotNull(cuentaCredito);
		}

		[TestMethod]
		public void Credito_Tiene_Banco_Emisor()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.BancoEmisor = "BancoEmisorPrueba";
			Assert.AreEqual("BancoEmisorPrueba", cuentaCredito.BancoEmisor);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCuentaException))]
		public void Excepcion_Credito_Tiene_Banco_Emisor_Vacio()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.BancoEmisor = "";
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCuentaException))]
		public void Excepcion_Credito_Tiene_Banco_Emisor_Nulo()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.BancoEmisor = null;
		}

		[TestMethod]
		public void Credito_Tiene_Numero_Tarjeta()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.NumeroTarjeta = "1234";
			Assert.AreEqual("1234", cuentaCredito.NumeroTarjeta);
		}

		[TestMethod]
		public void Excepcion_Numero_Tarjeta_Menor_Cuatro_Digitos()
		{
			Credito cuentaCredito = new Credito();

			Assert.ThrowsException<DomainCuentaException>(() =>
			{
				cuentaCredito.NumeroTarjeta = "123";
			});
		}

		[TestMethod]
		public void Excepcion_Numero_Tarjeta_Mayor_Cuatro_Digitos()
		{
			Credito cuentaCredito = new Credito();

			Assert.ThrowsException<DomainCuentaException>(() =>
			{
				cuentaCredito.NumeroTarjeta = "12345";
			});
		}

		[TestMethod]
		public void Credito_Tiene_CreditoDisponible()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.CreditoDisponible = 1000;
			Assert.AreEqual(1000, cuentaCredito.CreditoDisponible);
		}

		[TestMethod]
		public void Excepcion_CreditoDisponible_Negativo()
		{
			Credito cuentaCredito = new Credito();

			Assert.ThrowsException<DomainCuentaException>(() =>
			{
				cuentaCredito.CreditoDisponible = -1.1;
			});
		}

		[TestMethod]
		public void Credito_Tiene_FechaCierre()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.FechaCierre = new System.DateTime(2020, 1, 1);
		}

		[TestMethod]
		public void Credito_Tiene_IngresoMonetario()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.CreditoDisponible = 0;
			cuentaCredito.IngresoMonetario(100);
			Assert.AreEqual(100, cuentaCredito.CreditoDisponible);
		}
		[TestMethod]
		public void Credito_Tiene_EgresoMonetario()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.CreditoDisponible = 1000;
			cuentaCredito.EgresoMonetario(100);
			Assert.AreEqual(900, cuentaCredito.CreditoDisponible);
		}

		[TestMethod]
		public void Obtener_FechaCiere()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.FechaCierre = new System.DateTime(2020, 1, 1);
			Assert.AreEqual(new System.DateTime(2020, 1, 1), cuentaCredito.FechaCierre);
		}

		[TestMethod]
		public void Credito_Tiene_ToString()
		{
			var credito = new Credito
			{ 
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1234",
				BancoEmisor = "MiBanco",
				FechaCierre = new System.DateTime(2025, 1, 1, 0, 0, 0)
			};
			string fechaAhora = System.DateTime.Now.ToString();
			string resultadoEsperado = "Pesos Uruguayos\n"+fechaAhora+"\n1000\n1234\nMiBanco\n1/1/2025 0:00:00\n";
			string resultado = credito.ToString();
			Assert.AreEqual(resultadoEsperado, resultado);
		}

		[TestMethod]
		public void Credito_Equals_Null()
		{

			var credito = new Credito
			{
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1234",
				BancoEmisor = "MiBanco",
				FechaCierre = new System.DateTime(2025, 1, 1, 0, 0, 0)
			};

			Object objeto = null;
			var objeto2 = new Object();
			
			Assert.IsFalse(credito.Equals(objeto));
			Assert.IsFalse(credito.Equals(objeto2));
			Assert.IsTrue(credito.Equals(credito));
		}

		public void Credito_Equals_Igual_BancoEmisor()
		{

			var credito1 = new Credito
			{
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1234",
				BancoEmisor = "MiBanco",
				FechaCierre = new System.DateTime(2025, 1, 1, 0, 0, 0)
			};

			var credito2 = new Credito
			{
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1234",
				BancoEmisor = "MiBanco",
				FechaCierre = new System.DateTime(2025, 1, 1, 0, 0, 0)
			};

			Assert.IsTrue(credito1.Equals(credito2));
		}

		public void Credito_Equals_Diferente_BancoEmisor()
		{

			var credito1 = new Credito
			{
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1234",
				BancoEmisor = "MiBanco",
				FechaCierre = new System.DateTime(2025, 1, 1, 0, 0, 0)
			};

			var credito2 = new Credito
			{
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1234",
				BancoEmisor = "MiBancoEmisor",
				FechaCierre = new System.DateTime(2025, 1, 1, 0, 0, 0)
			};

			Assert.IsFalse(credito1.Equals(credito2));
		}

	}
}
