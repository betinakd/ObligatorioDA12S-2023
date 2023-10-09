using Domain;

namespace DomainTest
{
    [TestClass]
    public class CategoriaGastoTest
    {
        [TestMethod]
        public void CategoriaGasto_No_Nulo()
        {
            var _categoria = new CategoriaGasto();
            Assert.IsNotNull(_categoria);
        }

        [TestMethod]
        public void CategoriaGasto_Constructor_No_Nulo()
        {
            Categoria cat = new Categoria();
            var categoria = new CategoriaGasto(cat);
            Assert.IsNotNull(categoria.Categoria);
        }

        [TestMethod]
        public void CategoriaGasto_Misma_Categoria()
        {
            var categoria = new Categoria();
            CategoriaGasto cg = new CategoriaGasto(categoria);
            Assert.AreEqual(cg.Categoria, categoria);
        }

        [TestMethod]
        public void CategoriaGasto_Constructor_Triple_No_Nulo()
        {
            Categoria categoria = new Categoria();
            double _montoAcumulado = 20;
            double porcentaje = 20;
            var cg = new CategoriaGasto(categoria, _montoAcumulado, porcentaje);
            Assert.IsNotNull(cg);
        }

        [TestMethod]
        public void CategoriaGasto_Constructor_Triple()
        {
            Categoria categoria = new Categoria();
            double _montoAcumulado = 20;
            double porcentaje = 20;
            var cg = new CategoriaGasto(categoria, _montoAcumulado, porcentaje);
            Assert.IsTrue(cg.MontoUsado == _montoAcumulado && cg.Categoria.Equals(categoria) && cg.Porcentaje == porcentaje);
        }

        [TestMethod]
        public void CategoriaGasto_Contructor_Cat()
        {
            Categoria categoria = new Categoria();
            var cg = new CategoriaGasto(categoria);
            Assert.IsTrue(cg.MontoUsado == 0 && cg.Categoria.Equals(categoria) && cg.Porcentaje == 0);
        }

        [TestMethod]
        public void CategoriaGasto_Mismos_Valores_Monto_Porcentaje()
        {
            double _montoAcumulado = 15;
            double _porcentaje = 15;
            Categoria _categoria = new Categoria();
            var cg = new CategoriaGasto(_categoria, _montoAcumulado, _porcentaje);
            Assert.AreEqual(cg.MontoUsado, cg.Porcentaje);
        }

        [TestMethod]
        public void CategoriaGasto_Equals()
        {
            double _montoAcumulado = 15;
            double _porcentaje = 15;
            Categoria _cat = new Categoria
            {
                EstadoActivo = true,
                Nombre = "cat",
                Tipo = TipoCategoria.Costo,
            };
            var cg = new CategoriaGasto(_cat,_montoAcumulado, _porcentaje);
            Assert.AreEqual(cg, cg);
        }

        [TestMethod]
        public void CategoriaGasto_Distinta_Cat()
        {
            double _montoAcumulado = 15;
            double _porcentaje = 15;
            Categoria _cat = new Categoria
            {
                EstadoActivo = true,
                Nombre = "cat",
                Tipo = TipoCategoria.Costo,
            };
            Categoria _cat2 = new Categoria
            {
                EstadoActivo = true,
                Nombre = "cat2",
                Tipo = TipoCategoria.Costo,
            };
            var cg = new CategoriaGasto(_cat, _montoAcumulado, _porcentaje);
            var cg2 = new CategoriaGasto(_cat2, _montoAcumulado, _porcentaje);
            Assert.AreNotEqual(cg, cg2);
        }
    }
}
