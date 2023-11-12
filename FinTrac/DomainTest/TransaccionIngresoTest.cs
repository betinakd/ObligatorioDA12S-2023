﻿using Domain;
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
		public void TransaccionIngreso_Clon_Cuenta_Credito()
		{
			var transaccionClon = transaccion2.ClonTransaccion();
			Assert.AreEqual(transaccion2.Titulo, transaccionClon.Titulo);
			Assert.AreEqual(transaccion2.Moneda, transaccionClon.Moneda);
			Assert.AreEqual(transaccion2.CuentaMonetaria, transaccionClon.CuentaMonetaria);
			Assert.AreEqual(transaccion2.CategoriaTransaccion, transaccionClon.CategoriaTransaccion);
			Assert.AreEqual(transaccion2.Monto, transaccionClon.Monto);
		}

		[TestMethod]
		public void TransaccionIngreso_Clon_Cuenta_Ahorro()
		{
			var transaccionClon = transaccion1.ClonTransaccion();
			Assert.AreEqual(transaccion1.Titulo, transaccionClon.Titulo);
			Assert.AreEqual(transaccion1.Moneda, transaccionClon.Moneda);
			Assert.AreEqual(transaccion1.CuentaMonetaria, transaccionClon.CuentaMonetaria);
			Assert.AreEqual(transaccion1.CategoriaTransaccion, transaccionClon.CategoriaTransaccion);
			Assert.AreEqual(transaccion1.Monto, transaccionClon.Monto);
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
	}
}
