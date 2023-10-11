using Domain;
using DomainExceptions;

namespace DomainTest
{
    [TestClass]
    public class TransaccionIngresoTest
    {
        private TransaccionIngreso transaccion1;
        [TestInitialize]
        public void InitTests()
        {
            transaccion1 = new TransaccionIngreso() { 
                Monto = 100,
				Titulo = "Transaccion1",
				Moneda = TipoCambiario.Dolar,
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
        public void TransaccionIngreso_Clon()
        { 
            var transaccionClon = transaccion1.ClonTransaccion(transaccion1);
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
