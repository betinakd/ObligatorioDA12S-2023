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
    }
}
