using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Reporte
    {
        private Usuario _user;
        private Espacio _MiEspacio;

        public Espacio MiEspacio { get { return _MiEspacio; } set { _MiEspacio = value; } }

        public Usuario User { get { return _user; } set { _user = value; } }


        /*
            Reporte de objetivos de gastos: Debe existir la posibilidad de generar el reporte 
            de objetivos del mes actual. El mismo debe indicar el monto definido, el monto 
            gastado hasta el momento (Dentro del mes) y si el objetivo se cumple o no, 
            para cada uno de los objetivos definidos por el usuario. 
        */
        //NOTA: Quiero retornar un lista de una clase "custom". Este objeto tendria los siguientes atributos:
        //MontoDefinido::Double, MontoGastado::Double, CumpleMonto::Bool
        public void ReporteObjetivosDeGastos()
        {
            DateTime _actualDate = DateTime.Now;
            int _mesActual = _actualDate.Month;
            //recorrer la lista de objetivos del Espacio, los obj me dan el titulo y la lista de categorias
            //tengo que recorrer la lista de las transacciones de este mes para poder calcular el monto para ver si me pase o no
            double _montoAcumulado = 0;
            double _montoMaximo = 0;
        }
    }
}
