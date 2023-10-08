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
            List<ObjetivoGasto> _reporteGastos = _reporte.ReporteObjetivosDeGastos();
            Assert.IsTrue(_reporteGastos.Count == 0);
        }

        [TestMethod]
        public void ReporteObjetivosGastos_No_Vacio()
        {
            var _reporte = new Reporte();
            Espacio _miEspacio = new Espacio();
            Usuario _admin = new Usuario
            {
                Contrasena = "1234567890Yuu",
                Correo = "mateo@gmail.com",
            };
            _miEspacio.Admin = _admin;
            Categoria _categoria = new Categoria
            {
                EstadoActivo = true,
                Tipo = TipoCategoria.Costo,
                Nombre = "Una categoria",
            };
            _miEspacio.AgregarCategoria(_categoria);
            Cuenta _cuenta = new Cuenta();
            _miEspacio.AgregarCuenta(_cuenta);
            Transaccion transaccion = new Transaccion
            {
                CategoriaTransaccion = _categoria,
                Monto = 10,
                Moneda = TipoCambiario.PesosUruguayos,
                Titulo = "Transaccion Prueba",
                CuentaMonetaria = _cuenta,
            };
            _miEspacio.AgregarTransaccion(transaccion);
            List<Categoria> _listCat = new List<Categoria>();
            _listCat.Add(_categoria);
            Objetivo _objetivo = new Objetivo
            {
                Categorias = _listCat,
                MontoMaximo = 100,
                Titulo = "Menos gastos",
            };
            _miEspacio.AgregarObjetivo(_objetivo);
            _reporte.MiEspacio = _miEspacio;
            List<ObjetivoGasto> ret = _reporte.ReporteObjetivosDeGastos();
            Assert.IsTrue(ret.Count == 1);
        }
    
        [TestMethod]
        public void ReporteObjetivoGasto_Mismo_ObjetivoGasto()
        {
            var _reporte = new Reporte();
            Espacio _miEspacio = new Espacio();
            Usuario _admin = new Usuario
            {
                Contrasena = "1234567890Yuu",
                Correo = "mateo@gmail.com",
            };
            _miEspacio.Admin = _admin;
            Categoria _categoria = new Categoria
            {
                EstadoActivo = true,
                Tipo = TipoCategoria.Costo,
                Nombre = "Una categoria",
            };
            _miEspacio.AgregarCategoria(_categoria);
            Cuenta _cuenta = new Cuenta();
            _miEspacio.AgregarCuenta(_cuenta);
            Transaccion transaccion = new Transaccion
            {
                CategoriaTransaccion = _categoria,
                Monto = 10,
                Moneda = TipoCambiario.PesosUruguayos,
                Titulo = "Transaccion Prueba",
                CuentaMonetaria = _cuenta,
            };
            _miEspacio.AgregarTransaccion(transaccion);
            List<Categoria> _listCat = new List<Categoria>();
            _listCat.Add(_categoria);
            Objetivo _objetivo = new Objetivo
            {
                Categorias = _listCat,
                MontoMaximo = 100,
                Titulo = "Menos gastos",
            };
            _miEspacio.AgregarObjetivo(_objetivo);
            _reporte.MiEspacio = _miEspacio;
            ObjetivoGasto og = new ObjetivoGasto(100, 10);
            og.Objetivo = _objetivo;
            List<ObjetivoGasto> lista = _reporte.ReporteObjetivosDeGastos();
            ObjetivoGasto toCompare = lista.First();
            Assert.AreNotEqual(og, toCompare);
        }
    }
}
