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
            miReporte._MiEspacio = _EspacioRepo;
            Assert.AreEqual(miReporte._MiEspacio, _EspacioRepo);
        }

        [TestMethod]
        public void Reporte_Usuario_No_Nulo()
        {
            var _user = new Usuario();
            Reporte miReporte = new Reporte();
            miReporte.User = _user;
            Assert.IsNull(miReporte.User);
        }
    }
}
