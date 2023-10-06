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
            int porcentaje = 20;
            var cg = new CategoriaGasto(categoria, _montoAcumulado, porcentaje);
            Assert.IsNotNull(cg);
        }
    }
}
