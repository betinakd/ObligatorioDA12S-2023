﻿using Domain;
using System.Data.SqlTypes;

namespace DomainTest
{
	[TestClass]
	public class CuentaTest
	{
		private Cuenta cuenta;

		[TestInitialize]
		public void Inicializar()
		{
			cuenta = new Cuenta();
		}

		[TestMethod]
		public void Nueva_Cuenta_No_Nula()
		{
			Assert.IsNotNull(cuenta);
		}

		[TestMethod]
		public void Cuenta_Tiene_Moneda_Pesos_Uruguayos()
		{
			cuenta.Moneda = TipoCambiario.PesosUruguayos;
			Assert.AreEqual(cuenta.Moneda, TipoCambiario.PesosUruguayos);
		}

		[TestMethod]
		public void Cuenta_Tiene_Moneda_Dolar()
		{
			cuenta.Moneda = TipoCambiario.Dolar;
			Assert.AreEqual(cuenta.Moneda, TipoCambiario.Dolar);
		}

		[TestMethod]
		public void Cuenta_Tiene_FechaCreacion()
		{
			Assert.IsNotNull(cuenta.FechaCreacion);
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Excepcion_Recibir_Deposito()
		{
			cuenta.IngresoMonetario(100);
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Excepcion_Realizar_Deposito()
		{
			cuenta.EgresoMonetario(100);
		}

		[TestMethod]
		public void Cuenta_Tiene_ToString()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.Moneda = TipoCambiario.PesosUruguayos;
			string fecha = DateTime.Now.ToString();
			Assert.AreEqual(cuenta.ToString(), "Pesos Uruguayos\n" + fecha+ "\n");
		}

		[TestMethod]
		public void Cuenta_Tiene_ToString_Dolar()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.Moneda = TipoCambiario.Dolar;
			string fecha = DateTime.Now.ToString();
			Assert.AreEqual(cuenta.ToString(), "Dolar\n" + fecha + "\n");
		}

	}
}