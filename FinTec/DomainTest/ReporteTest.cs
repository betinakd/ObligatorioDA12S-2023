﻿using Domain;

namespace DomainTest
{
    [TestClass]
    public class ReporteTest
    {
        [TestMethod]
        public void Reporte_No_Nulo()
        {
            var rep1 = new Reporte();

            Assert.IsNotNull(rep1);
        }

        [TestMethod]
        public void Reporte_Tiene_Espacio()
        {
            var _EspacioRepo = new Espacio();
            Reporte miReporte = new Reporte();
            miReporte.MiEspacio = _EspacioRepo;
            Assert.AreEqual(miReporte.MiEspacio, _EspacioRepo);
        }

    }
}
