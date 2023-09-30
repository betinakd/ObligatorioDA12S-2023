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
        [ExpectedException(typeof(ArgumentException))]
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
    }
}
