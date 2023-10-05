using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainTest
{
    [TestClass]
    public class TransaccionesTest
    {
        [TestMethod]
        public void Nueva_Transaccion()
        {
            var transaccion = new Transaccion();
            Assert.IsNotNull(transaccion);
        }

        [TestMethod]    
        public void Titulo_Transaccion() 
        {
            var transaccion = new Transaccion();
            string titulo = "Transaccion1";
            transaccion.Titulo= titulo;
            Assert.AreEqual(titulo,transaccion.Titulo);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Titulo_Transaccion_Vacio()
        {
            var transaccion = new Transaccion();
            string titulo = "";
            transaccion.Titulo = titulo;
        }

        [TestMethod]
        public void Tiene_Fecha_Transaccion() 
        {
            var transaccion = new Transaccion();
            Assert.IsNotNull(transaccion.FechaTransaccion);
        }

        [TestMethod]
        public void Monto_Transaccion()
        {
            var transaccion = new Transaccion();
            double monto = 100;
            transaccion.Monto = monto;
            Assert.AreEqual(monto, transaccion.Monto);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Monto_Mayor_Cero() 
        {
            var transaccion = new Transaccion();
            double monto = 0;
            transaccion.Monto = monto;
        }

        [TestMethod]
        public void Moneda_Transaccion()
        {
            var transaccion = new Transaccion();
            transaccion.Moneda = TipoCambiario.Dolar;
            Assert.AreEqual(TipoCambiario.Dolar, transaccion.Moneda);
        }

        [TestMethod]
        public void Cuenta_Transaccion() 
        {
            var transaccion = new Transaccion();
            Cuenta cuenta = new Cuenta();
            cuenta.Moneda = TipoCambiario.Dolar;
            transaccion.Moneda = TipoCambiario.Dolar;
            transaccion.CuentaMonetaria = cuenta;
            Assert.AreEqual(cuenta, transaccion.CuentaMonetaria);
        }

        [TestMethod]
        public void Categoria_Transaccion() 
        {
            var transaccion = new Transaccion();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Costo;
            categoria.EstadoActivo = true;
            transaccion.CategoriaTransaccion = categoria;
            Assert.AreEqual(categoria, transaccion.CategoriaTransaccion);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Categoria_Inactiva_Transaccion()
        {
            var transaccion = new Transaccion();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Costo;
            categoria.EstadoActivo = false;
            transaccion.CategoriaTransaccion = categoria;            
        }

        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Tipo_Moneda_Distinto_Cuenta()
        {
            var transaccion = new Transaccion();
            Cuenta cuenta = new Cuenta();
            cuenta.Moneda = TipoCambiario.Dolar;
            transaccion.Moneda = TipoCambiario.PesosUruguayos;
            transaccion.CuentaMonetaria = cuenta;
            Assert.AreEqual(cuenta.Moneda, transaccion.Moneda);
        }
    }
}
