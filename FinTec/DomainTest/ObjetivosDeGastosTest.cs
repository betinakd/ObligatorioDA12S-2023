using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainTest
{
    [TestClass]
    public class ObjetivosDeGastosTest
    {
        [TestMethod]
        public void Nuevo_Objetivo()
        {
            var objetivo = new ObjetivosDeGastos();
            Assert.IsNotNull(objetivo);
        }
    }
}
