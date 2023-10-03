using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest
{
    [TestClass]
    public class TransaccionIngresoTest
    {
        [TestMethod]
        public void Nueva_TransaccionIngreso()
        {
            var transaccion = new TransaccionIngreso();
            Assert.IsNotNull(transaccion);
        }

        [TestMethod]
        public void Titulo_TransaccionIngreso()
        {
            var transaccion = new TransaccionIngreso();
            string titulo = "Transaccion1";
            transaccion.Titulo = titulo;
            Assert.AreEqual(titulo, transaccion.Titulo);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Titulo_TransaccionI_Vacio()
        {
            var transaccion = new TransaccionIngreso();
            string titulo = "";
            transaccion.Titulo = titulo;
        }

        [TestMethod]
        public void Tiene_Fecha_TransaccionI()
        {
            var transaccion = new TransaccionIngreso();
            Assert.IsNotNull(transaccion.FechaTransaccion);
        }

        [TestMethod]
        public void Monto_TransaccionI()
        {
            var transaccion = new TransaccionIngreso();
            double monto = 100;
            transaccion.Monto = monto;
            Assert.AreEqual(monto, transaccion.Monto);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Monto_Mayor_A_Cero()
        {
            var transaccion = new TransaccionIngreso();
            double monto = 0;
            transaccion.Monto = monto;
        }
    }
}
