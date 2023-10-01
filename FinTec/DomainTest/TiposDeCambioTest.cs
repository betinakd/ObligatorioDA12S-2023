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
    }
}
