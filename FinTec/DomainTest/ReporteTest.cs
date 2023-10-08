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
            miReporte.MiEspacio = _EspacioRepo;
            Assert.AreEqual(miReporte.MiEspacio, _EspacioRepo);
        }

        [TestMethod]
        public void ReporteObjetivosGastos_Vacio()
        {
            Espacio _miEspacio = new Espacio();
            Usuario _admin = new Usuario
            {
                Contrasena = "1234567890Yuu",
                Correo = "mateo@gmail.com",
            };
            _miEspacio.Admin = _admin;
            var _reporte = new Reporte();
            _reporte.MiEspacio = _miEspacio;
            //List<ObjetivoGasto> _objGastoVacio = new List<ObjetivoGasto>();
            List<ObjetivoGasto> _reporteGastos = _reporte.ReporteObjetivosDeGastos();
            Assert.IsFalse(_reporteGastos.Count == 0);
        }

    }
}
