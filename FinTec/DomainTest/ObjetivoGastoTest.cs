using Domain;

namespace DomainTest
{
    [TestClass]
    public class ObjetivoGastoTest
    {
        [TestMethod]
        public void ObjetivoGasto_No_Vacio()
        {
            var _objetivoGasto = new ObjetivoGasto();
            Assert.IsNotNull(_objetivoGasto);
        }

        [TestMethod]
        public void ObjetivoGasto_Cumple()
        {
            double valorEsperado = 5;
            double valorAcumulado = 4;
            var _objetivoGasto = new ObjetivoGasto(valorEsperado, valorAcumulado);
            Assert.IsTrue(_objetivoGasto.MontoCumpido());
        }

        [TestMethod]
        public void ObjetivoGasto_No_Cumple()
        {
            double valorEsperado = 5;
            double valorAcumulado = 7;
            var _objetivoGasto = new ObjetivoGasto(valorEsperado, valorAcumulado);
            Assert.IsTrue(_objetivoGasto.MontoCumpido());
        }
    }
}
