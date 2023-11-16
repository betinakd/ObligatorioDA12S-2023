using DTO;
using Dominio;
using Excepcion;
using LogicaNegocio;
using DTO.EnumsDTO;

namespace Controlador
{
	public class ControladorCuenta
	{
		private EspacioLogica _espacioLogic;
		public ControladorCuenta(EspacioLogica espacioLogic)
		{
			_espacioLogic = espacioLogic;
		}

		public List<AhorroDTO> AhorrosDeEspacio(int espacioId)
		{
			Espacio espacio = _espacioLogic.EncontrarEspacio(espacioId);
			List<AhorroDTO> ahorros = new List<AhorroDTO>();

			ahorros = espacio.Cuentas
				.OfType<Ahorro>()
				.Select(ahorro => new AhorroDTO
				{
					Id = ahorro.Id,
					Nombre = ahorro.Nombre,
					Saldo = ahorro.Saldo,
					FechaCreacion = ahorro.FechaCreacion,
					Moneda = ConversorMoneda(ahorro.Moneda),
				})
				.ToList();
			return ahorros;
		}

		public List<CreditoDTO> CreditosDeEspacio(int espacioId)
		{
			Espacio espacio = _espacioLogic.EncontrarEspacio(espacioId);
			List<CreditoDTO> creditos = new List<CreditoDTO>();

			creditos = espacio.Cuentas
				.OfType<Credito>()
				.Select(credito => new CreditoDTO
				{
					Id = credito.Id,
					BancoEmisor = credito.BancoEmisor,
					NumeroTarjeta = credito.NumeroTarjeta,
					FechaCreacion = credito.FechaCreacion,
					FechaCierre = credito.FechaCierre,
					Saldo = credito.Saldo,
					Moneda = ConversorMoneda(credito.Moneda),
				})
				.ToList();
			return creditos;
		}

		public string EliminarAhorro(int espacioId, AhorroDTO cuenta)
		{
			string mensaje = "";
			try
			{
				Cuenta ahorro = new Ahorro
				{
					Id = cuenta.Id,
					Nombre = cuenta.Nombre,
				};
				_espacioLogic.EliminarCuentaDeEspacio(espacioId, ahorro);
			}
			catch (DominioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		public string EliminarCredito(int espacioId, CreditoDTO cuenta)
		{
			string mensaje = "";
			try
			{
				Cuenta credito = new Credito
				{
					Id = cuenta.Id,
					BancoEmisor = cuenta.BancoEmisor,
					NumeroTarjeta = cuenta.NumeroTarjeta,
					FechaCierre = cuenta.FechaCierre,
				};
				_espacioLogic.EliminarCuentaDeEspacio(espacioId, credito);
			}
			catch (DominioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		public string ModificarAhorro(int espacioId, AhorroDTO ahorroModificado)
		{
			string mensaje = "";
			try
			{
				Ahorro cuenta = new Ahorro
				{
					Id = ahorroModificado.Id,
					Nombre = ahorroModificado.Nombre,
				};
				_espacioLogic.ModificarCuentaDeEspacio(espacioId, cuenta);
			}
			catch (DominioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		public string ModificarCredito(int espacioId, CreditoDTO ahorroModificado)
		{
			string mensaje = "";
			try
			{
				Credito cuenta = new Credito
				{
					Id = ahorroModificado.Id,
					NumeroTarjeta = ahorroModificado.NumeroTarjeta,
					BancoEmisor = ahorroModificado.BancoEmisor,
					FechaCierre = ahorroModificado.FechaCierre,
				};
				_espacioLogic.ModificarCuentaDeEspacio(espacioId, cuenta);
			}
			catch (DominioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		public string ModificarCreditoFechaCierre(int espacioId, CreditoDTO ahorroModificado)
		{
			string mensaje = "";
			try
			{
				Espacio espacio = _espacioLogic.EncontrarEspacio(espacioId);
				Cuenta cuenta = espacio.Cuentas.Find(cuenta => cuenta.Id == ahorroModificado.Id);
				cuenta.ModificarFecha(ahorroModificado.FechaCierre);
				_espacioLogic.UpdateEspacio(espacio);
			}
			catch (DominioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		private TipoCambiario ConversorMonedaDTO(TipoCambiarioDTO moneda)
		{
			TipoCambiario conversion;
			if (moneda == TipoCambiarioDTO.Dolar)
			{
				conversion = TipoCambiario.Dolar;
			}
			else if (moneda == TipoCambiarioDTO.Euro)
			{
				conversion = TipoCambiario.Euro;
			}
			else
			{
				conversion = TipoCambiario.PesosUruguayos;
			}
			return conversion;
		}

		private TipoCambiarioDTO ConversorMoneda(TipoCambiario moneda)
		{
			TipoCambiarioDTO conversion;
			if (moneda == TipoCambiario.Dolar)
			{
				conversion = TipoCambiarioDTO.Dolar;
			}
			else if (moneda == TipoCambiario.Euro)
			{
				conversion = TipoCambiarioDTO.Euro;
			}
			else
			{
				conversion = TipoCambiarioDTO.PesosUruguayos;
			}
			return conversion;
		}

		public string CrearAhorro(int espacioId, AhorroDTO ahorro)
		{
			string mensaje = "";
			try
			{
				Cuenta ahorroDTO = new Ahorro
				{
					Nombre = ahorro.Nombre,
					Saldo = ahorro.Saldo,
					FechaCreacion = ahorro.FechaCreacion,
					Moneda = ConversorMonedaDTO(ahorro.Moneda),
				};
				_espacioLogic.CrearCuenta(espacioId, ahorroDTO);
			}
			catch (DominioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			catch (LogicaNegocioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		public string CrearCredito(int espacioId, CreditoDTO credito)
		{
			string mensaje = "";
			try
			{
				Cuenta creditoDTO = new Credito
				{
					BancoEmisor = credito.BancoEmisor,
					NumeroTarjeta = credito.NumeroTarjeta,
					FechaCierre = credito.FechaCierre,
					Saldo = credito.Saldo,
					FechaCreacion = credito.FechaCreacion,
					Moneda = ConversorMonedaDTO(credito.Moneda),
				};
				_espacioLogic.CrearCuenta(espacioId, creditoDTO);
			}
			catch (DominioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			catch(LogicaNegocioEspacioExcepcion e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}
	}
}
