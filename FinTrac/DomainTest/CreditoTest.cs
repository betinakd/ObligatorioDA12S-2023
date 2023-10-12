using Models.Domain;
using Models.Excepcion;

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
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Credito_Tiene_Banco_Emisor_Vacio()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.BancoEmisor = "";
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
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

			Assert.ThrowsException<DomainEspacioException>(() =>
			{
				cuentaCredito.NumeroTarjeta = "123";
			});
		}

		[TestMethod]
		public void Excepcion_Numero_Tarjeta_Mayor_Cuatro_Digitos()
		{
			Credito cuentaCredito = new Credito();

			Assert.ThrowsException<DomainEspacioException>(() =>
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

			Assert.ThrowsException<DomainEspacioException>(() =>
			{
				cuentaCredito.CreditoDisponible = -1.1;
			});
		}

		[TestMethod]
		public void Credito_Tiene_FechaCierre()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.FechaCierre = new System.DateTime(2029, 1, 1);
		}

		/*[TestMethod]
		public void Credito_Tiene_IngresoMonetario()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.CreditoDisponible = 5;
			cuentaCredito.IngresoMonetario(100);
			Assert.AreEqual(105, cuentaCredito.CreditoDisponible);
		}
		[TestMethod]
		public void Credito_Tiene_EgresoMonetario()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.CreditoDisponible = 1000;
			cuentaCredito.EgresoMonetario(100);
			Assert.AreEqual(900, cuentaCredito.CreditoDisponible);
		}*/

		[TestMethod]
		public void Obtener_FechaCiere()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.FechaCierre = new System.DateTime(2029, 1, 1);
			Assert.AreEqual(new System.DateTime(2029, 1, 1), cuentaCredito.FechaCierre);
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
			string resultadoEsperado = "Pesos Uruguayos - 1000 - 1234 - MiBanco";
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

		[TestMethod]
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

		[TestMethod]
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

		[TestMethod]
		public void Credito_Equals_Diferente_NumTarjeta()
		{
			var credito1 = new Credito
			{
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1236",
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

			Assert.IsFalse(credito1.Equals(credito2));
		}

		[TestMethod]
		public void Credito_Equals_Igual_NumTarjeta()
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

		[TestMethod]
		public void Caracter_Es_Num_True()
		{
			var cuenta = new Credito()
			{
				BancoEmisor = "MiBanco",
				NumeroTarjeta = "1234"
			};
			Assert.IsTrue(cuenta.CaracterEsNumero("45555"));
		}

		[TestMethod]
		public void Caracter_Es_Num_False()
		{
			Credito cuenta = new Credito()
			{
				BancoEmisor = "MiBanco",
				NumeroTarjeta = "1234"
			};
			Assert.IsFalse(cuenta.CaracterEsNumero("a4"));
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Num_Tarjeta_Es_String()
		{
			Credito cuenta = new Credito()
			{
				BancoEmisor = "MiBanco",
				NumeroTarjeta = "1234"
			};
			cuenta.NumeroTarjeta = "1EE23";
		}

		[TestMethod]
		public void Credito_Equals_Nulls()
		{
			Credito ahorro1 = new Credito { BancoEmisor = "Santanderr", NumeroTarjeta = "1011", Moneda = TipoCambiario.Dolar };

			Assert.IsFalse(ahorro1.Equals(null));
		}

		[TestMethod]
		public void Ahorro_Equals_Diferentes()
		{
			Credito ahorro1 = new Credito { BancoEmisor = "Santanderr", NumeroTarjeta = "1011", Moneda = TipoCambiario.Dolar };
			Credito ahorro2 = new Credito { BancoEmisor = "Santander", NumeroTarjeta = "1111", Moneda = TipoCambiario.Dolar };

			Assert.IsFalse(ahorro1.Equals(ahorro2));
		}

		[TestMethod]
		public void Ahorro_Equals_Iguales()
		{
			Credito ahorro1 = new Credito { BancoEmisor = "Santander", NumeroTarjeta = "1111", Moneda = TipoCambiario.Dolar };
			Credito ahorro2 = new Credito { BancoEmisor = "Santander", NumeroTarjeta = "1111", Moneda = TipoCambiario.Dolar };

			Assert.IsTrue(ahorro1.Equals(ahorro2));
		}

		[TestMethod]
		public void Modificar_Cuenta_Credito() { 
			var credito = new Credito
			{
				Moneda = TipoCambiario.PesosUruguayos,
				CreditoDisponible = 1000,
				NumeroTarjeta = "1234",
				BancoEmisor = "MiBanco",
				FechaCierre = new System.DateTime(2025, 1, 1, 0, 0, 0)
			};

			var credito2 = new Credito
			{
				Moneda = TipoCambiario.Dolar,
				CreditoDisponible = 2000,
				NumeroTarjeta = "4321",
				BancoEmisor = "MiBancoEmisor",
				FechaCierre = new System.DateTime(2026, 1, 1, 0, 0, 0)
			};

			credito.Modificar(credito2);
			Assert.IsTrue(credito.Equals(credito2));
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Credito_Tiene_Fecha_Cierre_Menor_Hoy()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.FechaCierre = new System.DateTime(2019, 1, 1);
		}
	}
}
