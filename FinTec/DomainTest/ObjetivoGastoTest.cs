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
            Assert.IsFalse(_objetivoGasto.MontoCumpido());
        }

        [TestMethod]
        public void ObjetivoGasto_No_Vacio_Un_Atributo()
        {
            double _valorEsperado = 1;
            var _objetivoGasto = new ObjetivoGasto(_valorEsperado);
            Assert.IsNotNull(_objetivoGasto);
        }

        [TestMethod]
        public void ObjetivoGasto_No_Vacio_Dos_Atributos()
        {
            double valorEsperado = 5;
            double valorAcumulado = 7;
            var _objetivoGasto = new ObjetivoGasto(valorEsperado, valorAcumulado);
            Assert.IsNotNull(_objetivoGasto); 
        }

        [TestMethod]
        public void ObjetivoGasto_Constructor_Vacio()
        {
            var og = new ObjetivoGasto();
            Assert.IsTrue(og.MontoEsperado == 0 && og.MontoAcumulado == 0);
        }

        [TestMethod]
        public void ObjetivoGasto_Contructor_Un_Parametro()
        {
            double montoEsp = 5;
            var og = new ObjetivoGasto(montoEsp);
            Assert.IsTrue(og.MontoEsperado == montoEsp && og.MontoAcumulado == 0);
        }

        [TestMethod]
        public void ObjetivoGasto_Constructor_Dos_Parametros()
        {
            double montoEsp = 5;
            double montoAc = 6;
            var og = new ObjetivoGasto(montoEsp, montoAc);
            Assert.IsTrue(og.MontoEsperado == montoEsp && og.MontoAcumulado == montoAc);
        }

        [TestMethod]
        public void ObjetivoGasto_Cumple_Equals()
        {
            double montoEsp = 5;
            double montoAc = 6;
            Objetivo obj = new Objetivo();
            var og1 = new ObjetivoGasto(montoEsp, montoAc);
            var og2 = new ObjetivoGasto(montoEsp, montoAc);
            og1.Objetivo = obj;
            og2.Objetivo = obj;
            Assert.IsTrue(og1.Equals(og2));
        }

        [TestMethod]
        public void ObjetivoGasto_No_Cumple_Equals()
        {
            double montoEsp = 5;
            double montoAc = 6;
            Objetivo obj = new Objetivo();
            var og1 = new ObjetivoGasto(montoEsp, montoAc);
            var og2 = new ObjetivoGasto(montoEsp, montoEsp);
            og1.Objetivo = obj;
            og2.Objetivo = obj;
            Assert.IsTrue(og1.Equals(og2));
        }
    }
}
