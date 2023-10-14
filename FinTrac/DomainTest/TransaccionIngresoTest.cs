using Domain;
using Excepcion;

namespace DomainTest
{
    [TestClass]
    public class TransaccionIngresoTest
    {
        private TransaccionIngreso transaccion1;
        private TransaccionIngreso transaccion2;
        private Credito credito1;
        private Ahorro ahorro1;

        [TestInitialize]
        public void InitTests()
        {
            ahorro1 = new Ahorro()
            {
                Nombre = "Ahorro1",
                Moneda = TipoCambiario.Dolar,
                Monto = 100,
            };
            credito1 = new Credito()
            {
                NumeroTarjeta = "4444",
                Moneda = TipoCambiario.Dolar,
                CreditoDisponible = 100,
                FechaCierre = DateTime.Today.AddDays(4),
                BancoEmisor = "Banco1",
			};
            transaccion1 = new TransaccionIngreso() { 
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
        [ExpectedException(typeof(DomainEspacioException))]
        public void Tipo_Categoria_Distinto_TransaccionI()
        {
            var transaccion = new TransaccionIngreso();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Costo;
            categoria.EstadoActivo = true;
            transaccion.CategoriaTransaccion = categoria;
        }


        [TestMethod]
        [ExpectedException(typeof(DomainEspacioException))]
        public void Categoria_Inactiva_TransaccionC()
        {
            var transaccion = new TransaccionIngreso();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Ingreso;
            categoria.EstadoActivo = false;
            transaccion.CategoriaTransaccion = categoria;
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
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Categoria_Nula()
		{
			TransaccionIngreso transaccion = new TransaccionIngreso();
			transaccion.CategoriaTransaccion = null;
		}

        [TestMethod]
        public void TransaccionIngreso_Clon_Cuenta_Credito()
        { 
            var transaccionClon = transaccion1.ClonTransaccion();
			Assert.AreEqual(transaccion1.Titulo, transaccionClon.Titulo);
			Assert.AreEqual(transaccion1.Moneda, transaccionClon.Moneda);
			Assert.AreEqual(transaccion1.CuentaMonetaria, transaccionClon.CuentaMonetaria);
			Assert.AreEqual(transaccion1.CategoriaTransaccion, transaccionClon.CategoriaTransaccion);
			Assert.AreEqual(transaccion1.Monto, transaccionClon.Monto);
		}

		public void TransaccionIngreso_Clon_Cuenta_Ahorro()
		{
			var transaccionClon = transaccion2.ClonTransaccion();
			Assert.AreEqual(transaccion2.Titulo, transaccionClon.Titulo);
			Assert.AreEqual(transaccion2.Moneda, transaccionClon.Moneda);
			Assert.AreEqual(transaccion2.CuentaMonetaria, transaccionClon.CuentaMonetaria);
			Assert.AreEqual(transaccion2.CategoriaTransaccion, transaccionClon.CategoriaTransaccion);
			Assert.AreEqual(transaccion2.Monto, transaccionClon.Monto);
		}

		[TestMethod]
        public void TransaccionIngreso_Tiene_Cuenta_Monetaria_Valida()
        {
            transaccion1.CuentaMonetaria = new Ahorro()
            {
                Nombre = "Cuenta1",
                Moneda = TipoCambiario.Dolar,
                Monto = 100,
            };
        }

        [TestMethod]
        [ExpectedException(typeof(DomainEspacioException))]
        public void TransaccionIngreso_Tiene_Cuenta_Monetaria_Invalida()
        {
			transaccion1.CuentaMonetaria = new Ahorro()
            {
				Nombre = "Cuenta1",
				Moneda = TipoCambiario.PesosUruguayos,
				Monto = 100,
			};
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void TransaccionIngreso_Tiene_Cuenta_Monetaria_Nula()
		{
            transaccion1.CuentaMonetaria = null;
		}
	}
}
