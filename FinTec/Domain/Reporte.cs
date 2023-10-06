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
        public List<ObjetivoGasto> ReporteObjetivosDeGastos()
        {
            DateTime _actualDate = DateTime.Now;
            int _mesActual = _actualDate.Month;
            //recorrer la lista de objetivos del Espacio, los obj me dan el titulo y la lista de categorias
            //tengo que recorrer la lista de las transacciones de este mes para poder calcular el monto para ver si me pase o no
            List<ObjetivoGasto> ret = new List<ObjetivoGasto>();
            List<Objetivo> objetivos = MiEspacio.Objetivos;
            foreach (Objetivo o in objetivos)
            {
                List<Transaccion> transacciones = MiEspacio.Transacciones;
                ObjetivoGasto objetivo = new ObjetivoGasto(o.MontoMaximo);
                objetivo.Objetivo = o;
                double _montoAcumulado = 0;
                foreach (Transaccion t in transacciones)
                {
                    if ((o.Categorias.Contains(t.CategoriaTransaccion)) && (t.FechaTransaccion.Month == _mesActual))
                    {
                        _montoAcumulado += t.Monto;
                    }
                }
                objetivo.MontoAcumulado = _montoAcumulado;
                ret.Add(objetivo);
            }
            return ret;
        }

        /*
            Reporte de gastos por categoría dentro del mes, para todos los gastos del mes seleccionado, 
            se debe mostrar el total gastado por categoría, como también el % sobre el total de gasto. 
            Ejemplo:
            Para el mes de Setiembre: 
                - Educación: 20.000 => 40% 
                - Salidas: 10.000 =>20% 
                - Ropa: 10.000 => 20% 
                - Supermercado: 10.000 => 20% 
        */
        /*
        Estrategia:
            - Recorrer lista de categorias de MiEspacio
            - Calcular el total gastado por mes
            - Recorrer lista de transacciones de MiEspacio y filtrarlas por el mes
            - Solo elijo las transacciones que pertenecen a la categoria y uso su monto gastado
        Nota: Crear clase "custom" que guarde [Categoria, montoUsado, Porcentaje]
        */
        public void ReporteGastosCategoriaPorMes(int mes)
        {

        }
    }
}
