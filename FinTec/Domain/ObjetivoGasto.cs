namespace Domain
{
    public class ObjetivoGasto
    {
        private double _montoEsperado;
        private double _montoAcumulado;

        public double MontoEsperado { set { _montoEsperado = value; } get { return _montoEsperado; } }
        public double MontoAcumulado { set { _montoAcumulado = value; } get { return _montoAcumulado; } }
        
        public ObjetivoGasto() 
        {
            MontoEsperado = 0;
            MontoAcumulado = 0;
        }

        public ObjetivoGasto(double valorEsperado)
        {
            MontoEsperado = valorEsperado;
            MontoAcumulado = 0;
        }

        public ObjetivoGasto(double valorEsperado, double valorAcumulado)
        {
            MontoEsperado = valorEsperado;
            MontoAcumulado = valorEsperado;
        }
        
        public bool MontoCumpido()
        {
            return MontoEsperado < MontoAcumulado;
        }
    }
}
