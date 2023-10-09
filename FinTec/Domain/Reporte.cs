namespace Domain
{
    public class Reporte
    {
        private Espacio _MiEspacio;

        public Espacio MiEspacio { get { return _MiEspacio; } set { _MiEspacio = value; } }

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
                bool controlEntrada = false;
                foreach (Transaccion t in transacciones)
                {
                    if ((o.Categorias.Contains(t.CategoriaTransaccion)) && (t.FechaTransaccion.Month.Equals(_mesActual)) && (t.FechaTransaccion.Year.Equals(_yearActual)) && (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo)))
                    {
                        controlEntrada = true;
                        _montoAcumulado += t.Monto;
                    }
                }
                if (controlEntrada)
                {
                    objetivo.MontoAcumulado = _montoAcumulado;
                    ret.Add(objetivo);
                }
            }
            return ret;
        }

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
                double _porcentaje = (_montoAcumulado * 100) / _montoTotal;
                CategoriaGasto cg = new CategoriaGasto(c, _montoAcumulado, _porcentaje);
                _retList.Add(cg);
            }
            return _retList;
        }

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

        public List<Transaccion> ReporteGastosTarjeta(string _nroTarjeta)
        {
            List<Transaccion> listRet = new List<Transaccion>();
            List<Cuenta> cuentas = MiEspacio.Cuentas;
            DateTime actualDate = DateTime.Now;
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
                                        firstDate = new DateTime(creditAccount.FechaCierre.Year, creditAccount.FechaCierre.Month - 1, creditAccount.FechaCierre.Day + 1);
                                        lastDate = creditAccount.FechaCierre;
                                        if (TransaccionDentroDelScope(t, firstDate, lastDate))
                                        {
                                            listRet.Add(t);
                                        }
                                    } else
                                    {
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

        public double BalanceCuentas(Ahorro account)
        {
            double saldoCuenta = account.Monto + sumatoriaIngresos(account) - sumatoriaCostos(account);
            return saldoCuenta;
        }

        public double sumatoriaIngresos(Ahorro account)
        {
            double sum = 0;
            List<Transaccion> transacciones = MiEspacio.Transacciones;
            foreach(Transaccion t in transacciones)
            {
                if (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Ingreso) && (account.Equals((Ahorro)t.CuentaMonetaria)))
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
            foreach (Transaccion t in transacciones)
            {
                if (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo) && ((Ahorro)t.CuentaMonetaria).Equals(account))
                {
                    sum += t.Monto;
                }
            }
            return sum;
        }
    }
}
