namespace Domain
{
    public class Reporte
    {
        private Usuario _user;
        private Espacio _MiEspacio;

        public Espacio MiEspacio { get { return _MiEspacio; } set { _MiEspacio = value; } }

        public Usuario User { get { return _user; } set { _user = value; } }

        //NOTA: Quiero retornar un lista de una clase "custom". Este objeto tendria los siguientes atributos:
        //MontoDefinido::Double, MontoGastado::Double, CumpleMonto::Bool
        public List<ObjetivoGasto> ReporteObjetivosDeGastos()
        {
            DateTime _actualDate = DateTime.Now;
            int _mesActual = _actualDate.Month;
            int _yearActual = _actualDate.Year;
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
                    if ((o.Categorias.Contains(t.CategoriaTransaccion)) && (t.FechaTransaccion.Month.Equals(_mesActual)) && (t.FechaTransaccion.Year.Equals(_yearActual)) && (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo)))
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
            B)
            Reporte de gastos por categoría dentro del mes, para todos los gastos del mes seleccionado, 
            se debe mostrar el total gastado por categoría, como también el % sobre el total de gasto. 
            Ejemplo:
            Para el mes de Setiembre: 
                - Educación: 20.000 => 40% 
                - Salidas: 10.000 =>20% 
                - Ropa: 10.000 => 20% 
                - Supermercado: 10.000 => 20% 

            Estrategia:
                - Recorrer lista de categorias de MiEspacio
                - Calcular el total gastado por mes
                - Recorrer lista de transacciones de MiEspacio y filtrarlas por el mes y categoria.
                - Solo elijo las transacciones que pertenecen a la categoria y uso su monto gastado

            Nota: Crear clase "CategoriaGasto" que guarde [Categoria, montoUsado, Porcentaje]
        */
        public List<CategoriaGasto> ReporteGastosCategoriaPorMes(int mes)
        {
            List<CategoriaGasto> _retList = new List<CategoriaGasto>();
            List<Categoria> _categorias = MiEspacio.Categorias;
            double _montoTotal = Calcular_MontoTotal(mes);
            foreach (Categoria c in _categorias)
            {
                double _montoAcumulado = 0;
                List<Transaccion> transacciones = MiEspacio.Transacciones;
                foreach (Transaccion t in transacciones)
                {
                    if ((t.CategoriaTransaccion.Equals(c)) && (t.FechaTransaccion.Month.Equals(mes)) && (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo)))
                    {
                        _montoAcumulado += t.Monto;
                    }
                }
                double _porcentaje = (_montoTotal * 100) / _montoAcumulado;
                CategoriaGasto cg = new CategoriaGasto(c, _montoAcumulado, _porcentaje);
                _retList.Add(cg);
            }
            return _retList;
        }

        //pre: Recibe un mes como numero
        //pos: Retorna el total gastado ese mes
        public double Calcular_MontoTotal(int mes)
        {
            double total = 0;
            List<Transaccion> _listTran = MiEspacio.Transacciones;
            foreach(Transaccion t in _listTran)
            {
                if ((t.FechaTransaccion.Month.Equals(mes)) && (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo)))
                {
                    total += t.Monto;
                }
            }
            return total;
        }

        /*
        C)
        Listado de gastos: El sistema deberá mostrar el listado de gastos ingresados, 
        pudiendo filtrarlos por categoría, rango de fechas y cuenta. 
        */
        public List<Transaccion> ListadoGastos(Categoria _catFiltro, DateTime _fechaIni, DateTime _fechaFin, Cuenta _cuentaFiltro)
        {
            List<Transaccion> _transacciones = MiEspacio.Transacciones;
            List<Transaccion> _retList = new List<Transaccion>();
            foreach(Transaccion t in _transacciones)
            {
                if (CumpleFiltroCategoria(t.CategoriaTransaccion, _catFiltro) && (TransaccionDentroDelScope(t, _fechaIni, _fechaFin)) && MismaCuenta(t.CuentaMonetaria, _cuentaFiltro))
                {
                    _retList.Add(t);
                }
            }
            return _retList;
        }

        public bool CumpleFiltroCategoria(Categoria _catFiltro, Categoria _catTrans)
        {
            return _catTrans.Equals(_catFiltro) && _catTrans.Tipo.Equals(_catFiltro.Tipo);
        }

        public bool MismaCuenta(Cuenta _cuenta1, Cuenta _cuenta2)
        {
            return _cuenta1.Equals(_cuenta2);
        }

        /*
        D)
        Reporte de gastos por tarjeta. Se deberá elegir una tarjeta y se mostrarán los gastos efectuados 
        con la misma para el mes actual. Para ello debe tomarse en cuenta la fecha de cierre de balance. 
        Por ejemplo, si la fecha de cierre de balance es el 20, el reporte debe tomar en cuenta solamente 
        los movimientos efectuados entre el día 21 del mes anterior y el día 20 del mes actual inclusive. 
        */
        /*
            Notas:
            Debo consultar las cuentas de credito y guardar en variables:
            - Fecha de cierre, tengo qe checkear si la fecha de cierre esta dentro del mes actual:
                -> Caso que si: Tengo que agarrar la fecha de cierre y y checkear el mes anterior. (ver ejemplo) 
                -> Caso que no: Simplemente chequear las transacciones del mes.
                Nota: Para esto tengo que trabajar en un rango de fechas.
            - Nros de la tarjeta
            - GASTOS de las transacciones para el mes actual.
        */
        public List<Transaccion> ReporteGastosTarjeta(string _nroTarjeta)
        {
            List<Transaccion> listRet = new List<Transaccion>();
            List<Cuenta> cuentas = MiEspacio.Cuentas;
            DateTime actualDate = DateTime.Now; //para chequear si esta dentro del scope
            foreach(Cuenta c in cuentas)
            {
                if (c is Credito)
                {
                    Credito creditAccount = (Credito)c;
                    if (creditAccount.NumeroTarjeta.Equals(_nroTarjeta))
                    {
                        List<Transaccion> listaTrans = MiEspacio.Transacciones;
                        foreach(Transaccion t in listaTrans)
                        {
                            if ((t.CuentaMonetaria is Credito) && MismaCuenta((Credito)t.CuentaMonetaria, creditAccount) && (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo)))
                            {
                                if (!CuentaVencida(creditAccount, actualDate))
                                {
                                    DateTime firstDate;
                                    DateTime lastDate;
                                    if (CuentaVenceEseMes(creditAccount, actualDate))
                                    {
                                        //tomar en cuenta los gastos del mes anterior
                                        firstDate = new DateTime(creditAccount.FechaCierre.Year, creditAccount.FechaCierre.Month - 1, creditAccount.FechaCierre.Day + 1);
                                        lastDate = creditAccount.FechaCierre;
                                        if (TransaccionDentroDelScope(t, firstDate, lastDate))
                                        {
                                            listRet.Add(t);
                                        }
                                    } else
                                    {
                                        //comportamiento normal, del primer dia del mes hasta el ultimo
                                        firstDate = new DateTime(actualDate.Year, actualDate.Month, 1);
                                        lastDate = firstDate.AddMonths(1);
                                        lastDate = lastDate.AddDays(-1);
                                        if (TransaccionDentroDelScope(t, firstDate, lastDate))
                                        {
                                            listRet.Add(t);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return listRet;
        }

        public bool CuentaVenceEseMes(Credito CA, DateTime actualDate)
        {
            DateTime fechaCierre = CA.FechaCierre;
            return fechaCierre.Month == actualDate.Month;
        }

        public bool CuentaVencida(Credito CA, DateTime actualDate)
        {
            DateTime fechaCierre = CA.FechaCierre;
            return fechaCierre < actualDate;
        }

        public bool TransaccionDentroDelScope(Transaccion t, DateTime firstDate, DateTime lastDate)
        {
            DateTime fechaTransaccion = t.FechaTransaccion;
            return (firstDate <= fechaTransaccion) && (fechaTransaccion <= lastDate);
        }

        /*
        E)
        Balance de cuentas (no incluye tarjetas). Dado una cuenta, el sistema deberá permitir ver el saldo 
        de la cuenta, que resulta de esta fórmula: 
        Monto inicial de la cuenta + sumatoria de ingresos - sumatoria de costos. 
        */
        public double BalanceCuentas(Ahorro account)
        {
            double saldoCuenta = account.Monto;
            saldoCuenta += sumatoriaIngresos(account);
            saldoCuenta -= sumatoriaCostos(account);
            return saldoCuenta;
        }

        public double sumatoriaIngresos(Ahorro account)
        {
            double sum = 0;
            List<Transaccion> transacciones = MiEspacio.Transacciones;
            foreach(Transaccion t in transacciones)
            {
                if (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Ingreso) && t.CuentaMonetaria.Equals(account))
                {
                    sum += t.Monto;
                }
            }
            return sum;
        }

        public double sumatoriaCostos(Ahorro account)
        {
            double sum = 0;
            List<Transaccion> transacciones = MiEspacio.Transacciones;
            foreach(Transaccion t in transacciones)
            {
                if (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo) && t.CuentaMonetaria.Equals(account))
                {
                    sum += t.Monto;
                }
            }
            return sum;
        }
    }
}
