using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainTest
{
    [TestClass]
    public class CambioTest
    {
        [TestMethod]
        public void Nuevo_TipoDeCambio()
        {
            var tipoDeCambio = new Cambio();
            Assert.IsNotNull(tipoDeCambio);
        }

        [TestMethod]
        public void TipoDeCambio_TieneFecha()
        {
            var tipoDeCambio = new Cambio();
            Assert.IsNotNull(tipoDeCambio.FechaDeCambio);
        }

        [TestMethod]
        public void Tipo_Dolar()
        {
            var tipoDeCambio = new Cambio();
            tipoDeCambio.Moneda = TipoCambiario.Dolar;
            Assert.AreEqual(TipoCambiario.Dolar, tipoDeCambio.Moneda);
        }

        [TestMethod]
        public void Valor_Moneda_PesosUy()
        {
            var tipoDeCambio = new Cambio();
            tipoDeCambio.Pesos = 200;
            Assert.AreEqual(200, tipoDeCambio.Pesos);
        }

        [TestMethod]
        public void Equals_Moneda_Null_Object_False()
        {
			var tipoDeCambio = new Cambio() 
            {
                Moneda = TipoCambiario.Dolar,
                Pesos = 50,
			};

			object objeto = new();
            Assert.IsFalse(tipoDeCambio.Equals(objeto));
			Assert.IsFalse(tipoDeCambio.Equals(null));
            Assert.IsTrue(tipoDeCambio.Equals(tipoDeCambio));
		}
    }
}
