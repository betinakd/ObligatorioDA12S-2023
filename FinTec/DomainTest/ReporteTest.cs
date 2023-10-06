using Domain;

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
            Assert.IsNotNull(miReporte.User);
        }

        [TestMethod]
        public void Reporte_Mismo_Usuario_Espacio()
        {
            var _espacio = new Espacio();
            Usuario _user = new Usuario();
            _espacio.Admin = _user;
            Reporte _reporte = new Reporte();
            _reporte.User = _user;
            Assert.AreNotEqual(_reporte.User, _espacio.Admin);
        }
    }
}
