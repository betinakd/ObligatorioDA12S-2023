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
    }
}
