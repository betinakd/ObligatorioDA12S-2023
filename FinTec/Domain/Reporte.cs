using DomainExceptions;

namespace Domain
{
    public class Reporte
    {
        private Espacio _MiEspacio;

        public Espacio MiEspacio { get { return _MiEspacio; } set { _MiEspacio = value; } }

        public List<ObjetivoGasto> ReporteObjetivosDeGastos()
        {
            List<ObjetivoGasto> ret = new List<ObjetivoGasto>();
            List<Objetivo> objetivos = MiEspacio.Objetivos;
            DateTime fechaActual = DateTime.Now;
            foreach (Objetivo o in objetivos)
            {
                List<Transaccion> transacciones = MiEspacio.Transacciones;
                ObjetivoGasto objetivo = new ObjetivoGasto(o.MontoMaximo);
                objetivo.Objetivo = o;
                double _montoAcumulado = 0;
                bool transaccionesAceptadas = false;
                foreach (Transaccion t in transacciones)
                {
					Cambio cambio = new Cambio();
					if (t.Moneda.Equals(TipoCambiario.Dolar))
					{
						cambio = t.EncontrarCambio(MiEspacio);
					}
					if (TransaccionEntraObjetivo(o, t) && TransaccionMismoYearYMes(t, fechaActual) && TransaccionCategoriaCosto(t))
                    {
						transaccionesAceptadas = true;
						if (t.Moneda.Equals(TipoCambiario.Dolar))
						{
							_montoAcumulado += t.Monto * cambio.Pesos;
						}
						else
						{
							_montoAcumulado += t.Monto;
						}
					}
                }
                if (transaccionesAceptadas)
                {
                    objetivo.MontoAcumulado = _montoAcumulado;
                    ret.Add(objetivo);
                }
            }
            return ret;
        }

        public bool TransaccionEntraObjetivo(Objetivo o, Transaccion t)
        {
            return (o.Categorias.Contains(t.CategoriaTransaccion));
        }

        public bool TransaccionMismoYearYMes(Transaccion t, DateTime fecha)
        {
            return (t.FechaTransaccion.Month.Equals(fecha.Month)) && (t.FechaTransaccion.Year.Equals(fecha.Year));
        }

        public bool TransaccionCategoriaCosto(Transaccion t)
        {
            return (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo));
        }

        public List<CategoriaGasto> ReporteGastosCategoriaPorMes(int mes)
        {
            List<CategoriaGasto> _reporteGastos = new List<CategoriaGasto>();
            List<Categoria> _categorias = MiEspacio.Categorias;
            double _montoTotal = Calcular_MontoTotal(mes);
            foreach (Categoria c in _categorias)
            {
                double _montoAcumulado = 0;
                List<Transaccion> transacciones = MiEspacio.Transacciones;
                bool transaccionAceptada = false;
                foreach (Transaccion t in transacciones)
                {
					Cambio cambio = new Cambio();
					if (t.Moneda.Equals(TipoCambiario.Dolar))
                    {
                        cambio = t.EncontrarCambio(MiEspacio);
                    }
                    if (TransaccionMismaCategoria(c, t) && TransaccionMismoMes(t, mes) && TransaccionCategoriaCosto(t))
                    {
                        transaccionAceptada = true;
                        if (t.Moneda.Equals(TipoCambiario.Dolar))
                        {
							_montoAcumulado += t.Monto * cambio.Pesos;
						} else
                        {
                            _montoAcumulado += t.Monto;
                        }
                    }
                }
                if (transaccionAceptada)
                {
					double _porcentaje = (_montoAcumulado * 100) / _montoTotal;
					CategoriaGasto cg = new CategoriaGasto(c, _montoAcumulado, _porcentaje);
					_reporteGastos.Add(cg);
				}
            }
            return _reporteGastos;
        }

        public bool TransaccionMismaCategoria(Categoria c, Transaccion t)
        {
            return (t.CategoriaTransaccion.Equals(c));
        }

        public bool TransaccionMismoMes(Transaccion t, int mes)
        {
            return (t.FechaTransaccion.Month.Equals(mes));
        }

        public double Calcular_MontoTotal(int mes)
        {
            double montoTotal = 0;
            List<Transaccion> _listTran = MiEspacio.Transacciones;
            foreach(Transaccion t in _listTran)
			{
				Cambio cambio = new Cambio();
				if (TransaccionMismoMes(t, mes) && TransaccionCategoriaCosto(t))
                {
					if (t.Moneda.Equals(TipoCambiario.Dolar))
					{
						cambio = t.EncontrarCambio(MiEspacio);
						montoTotal += t.Monto * cambio.Pesos;
					} else
                    {
                        montoTotal += t.Monto;
                    }					
                }
            }
            return montoTotal;
        }

        public List<Transaccion> ListadoGastos(Categoria _catFiltro, DateTime _fechaIni, DateTime _fechaFin, Cuenta _cuentaFiltro)
        {
            List<Transaccion> _transacciones = MiEspacio.Transacciones;
            List<Transaccion> _listaGastos = new List<Transaccion>();
            foreach(Transaccion t in _transacciones)
            {
                if (CumpleFiltroCategoria(t.CategoriaTransaccion, _catFiltro) && (TransaccionDentroDelScope(t, _fechaIni, _fechaFin)) && MismaCuenta(t.CuentaMonetaria, _cuentaFiltro))
                {
                    _listaGastos.Add(t);
                }
            }
            return _listaGastos;
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
            List<Transaccion> _reporteGastos = new List<Transaccion>();
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
                            if ((t.CuentaMonetaria is Credito) && MismaCuenta((Credito)t.CuentaMonetaria, creditAccount) && TransaccionCategoriaCosto(t))
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
                                            _reporteGastos.Add(t);
                                        }
                                    } else
                                    {
                                        firstDate = new DateTime(actualDate.Year, actualDate.Month, 1);
                                        lastDate = firstDate.AddMonths(1);
                                        lastDate = lastDate.AddDays(-1);
                                        if (TransaccionDentroDelScope(t, firstDate, lastDate))
                                        {
                                            _reporteGastos.Add(t);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return _reporteGastos;
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
            Cambio cambioUtilizado = BuscarCambioActual(account.FechaCreacion);
            double saldoCuenta;

			if (account.Moneda.Equals(TipoCambiario.PesosUruguayos))
            {
                saldoCuenta = account.Monto + SumatoriaIngresos(account, cambioUtilizado) - SumatoriaCostos(account, cambioUtilizado);
				return saldoCuenta;
			} else
            {
                saldoCuenta = (account.Monto * cambioUtilizado.Pesos) + SumatoriaIngresos(account, cambioUtilizado) - SumatoriaCostos(account, cambioUtilizado);
            }
            return saldoCuenta;
        }

        public double SumatoriaIngresos(Ahorro account, Cambio cambioUtilizado)
        {
            double _montoIngresos = 0;
            List<Transaccion> transacciones = MiEspacio.Transacciones;
            foreach(Transaccion t in transacciones)
            {
                if (TransaccionCategoriaIngreso(t) && MismaCuentaAhorro(account, (Ahorro)t.CuentaMonetaria))
                {
                    if (t.Moneda.Equals(TipoCambiario.Dolar))
                    {
						_montoIngresos += t.Monto * cambioUtilizado.Pesos;
					} else
                    {
						_montoIngresos += t.Monto;
					}
                }
            }
            return _montoIngresos;
        }

        public bool TransaccionCategoriaIngreso(Transaccion t)
        {
            return t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Ingreso);
        }

        public bool MismaCuentaAhorro(Ahorro c1, Ahorro c2)
        {
            return c1.Equals(c2);
        }

        public double SumatoriaCostos(Ahorro account, Cambio cambioUtilizado)
        {
            double _montoCostos = 0;
            List<Transaccion> transacciones = MiEspacio.Transacciones;
            foreach (Transaccion t in transacciones)
            {
                if (TransaccionCategoriaCosto(t) && MismaCuentaAhorro(account, (Ahorro)t.CuentaMonetaria))
                {
					if (t.Moneda.Equals(TipoCambiario.Dolar))
					{
						_montoCostos += t.Monto * cambioUtilizado.Pesos;
					}
					else
					{
						_montoCostos += t.Monto;
					}
				}
            }
            return _montoCostos;
        }

        public Cambio BuscarCambioActual(DateTime fecha)
        {
            Cambio cambioRet = new Cambio();
            foreach (Cambio cambio in MiEspacio.Cambios)
            {
                if (cambio.FechaDeCambio.Day == fecha.Day && cambio.FechaDeCambio.Month == fecha.Month && cambio.FechaDeCambio.Year == fecha.Year)
                {
                    cambioRet = cambio;
                }
            }
            return cambioRet;
        }
    }
}
