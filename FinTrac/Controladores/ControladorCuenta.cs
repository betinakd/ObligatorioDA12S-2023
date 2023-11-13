using DTO;
using Domain;
using Excepcion;
using BussinesLogic;
using DTO.EnumsDTO;

namespace Controlador
{
	public class ControladorCuenta
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;
		public ControladorCuenta(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public List<AhorroDTO> AhorrosDeEspacio(int espacioId)
		{
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
			List<AhorroDTO> ahorros = new List<AhorroDTO>();

			ahorros = espacio.Cuentas
				.OfType<Ahorro>()
				.Select(ahorro => new AhorroDTO
				{
					Id = ahorro.Id,
					Nombre = ahorro.Nombre,
					Monto = ahorro.Monto,
					FechaCreacion = ahorro.FechaCreacion,
					Moneda = ConversorMoneda(ahorro.Moneda),
				})
				.ToList();
			return ahorros;
		}

		public List<CreditoDTO> CreditosDeEspacio(int espacioId)
		{
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
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
					CreditoDisponible = credito.CreditoDisponible,
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
			catch (DomainEspacioException e)
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
			catch (DomainEspacioException e)
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
			catch (DomainEspacioException e)
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
				Cuenta cuenta = new Credito
				{
					Id = ahorroModificado.Id,
					NumeroTarjeta = ahorroModificado.NumeroTarjeta,
					BancoEmisor = ahorroModificado.BancoEmisor,
					FechaCierre = ahorroModificado.FechaCierre,
				};
				_espacioLogic.ModificarCuentaDeEspacio(espacioId, cuenta);
			}
			catch (DomainEspacioException e)
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
					Monto = ahorro.Monto,
					FechaCreacion = ahorro.FechaCreacion,
					Moneda = ConversorMonedaDTO(ahorro.Moneda),
				};
				_espacioLogic.CrearCuenta(espacioId, ahorroDTO);
			}
			catch (DomainEspacioException e)
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
					CreditoDisponible = credito.CreditoDisponible,
					FechaCreacion = credito.FechaCreacion,
					Moneda = ConversorMonedaDTO(credito.Moneda),
				};
				_espacioLogic.CrearCuenta(espacioId, creditoDTO);
			}
			catch (DomainEspacioException e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}
	}
}
