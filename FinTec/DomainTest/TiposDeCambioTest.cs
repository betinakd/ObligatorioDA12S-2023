using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainTest
{
    [TestClass]
    public class TiposDeCambioTest
    {
        [TestMethod]
        public void Nuevo_TipoDeCambio()
        {
            var tipoDeCambio = new TiposDeCambio();
            Assert.IsNotNull(tipoDeCambio);
        }

        [TestMethod]
        public void TipoDeCambio_TieneFecha()
        {
            var tipoDeCambio = new TiposDeCambio();
            Assert.IsNotNull(tipoDeCambio.FechaDeCambio);
        }

        [TestMethod]
        public void Tipo_Dolar()
        {
            var tipoDeCambio = new TiposDeCambio();
            tipoDeCambio.Moneda = TipoCambiario.Dolar;
            Assert.AreEqual(TipoCambiario.Dolar, tipoDeCambio.Moneda);
        }

        [TestMethod]
        public void Valor_Moneda_PesosUy()
        {
            var tipoDeCambio = new TiposDeCambio();
            tipoDeCambio.Pesos = 200;
            Assert.AreEqual(200, tipoDeCambio.Pesos);
        }
    }
}
