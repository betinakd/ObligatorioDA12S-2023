using Domain;

namespace EspacioReporte
{
    public class ObjetivoGasto
    {
        private Objetivo _objetivo;
        private double _montoEsperado;
        private double _montoAcumulado;


        public Objetivo Objetivo { get { return _objetivo; } set { _objetivo = value; } }
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
            MontoAcumulado = valorAcumulado;
        }
        
        public bool MontoCumpido()
        {
            return MontoEsperado > MontoAcumulado;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            };

            ObjetivoGasto og = (ObjetivoGasto)obj;
            return Objetivo.Equals(og.Objetivo) && MontoEsperado == og.MontoEsperado && MontoAcumulado == og.MontoAcumulado;
        }
    }
}
