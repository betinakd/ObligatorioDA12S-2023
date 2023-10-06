
namespace DomainTest
{
    [TestClass]
    public class ObjetivoGastoTest
    {
        [TestMethod]
        public void ObjetivoGasto_No_Vacio()
        {
            var _objetivoGasto = new ObjetivoTest();
            Assert.IsNull(_objetivoGasto);
        }
    }
}
