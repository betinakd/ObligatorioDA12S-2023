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
            int valorEsperado = 5;
            int valorAcumulado = 4;
            var _objetivoGasto = new ObjetivoGasto(valorEsperado, valorAcumulado);
            Assert.IsFalse(_objetivoGasto.MontoCumpido());
        }
    }
}
