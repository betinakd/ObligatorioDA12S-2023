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

        [TestMethod]
        public void Tiene_Titulo()
        {
            var objetivo = new ObjetivosDeGastos();
            objetivo.Titulo = "Objetivo 1";
            Assert.AreEqual("Objetivo 1", objetivo.Titulo);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Tiene_Titulo_Vacio()
        {
            var objetivo = new ObjetivosDeGastos();
            objetivo.Titulo = "";
        }
    }
}
