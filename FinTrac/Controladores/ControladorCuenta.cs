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
				})
				.ToList();
			return creditos;
		}

		public string EliminarAhorro(int espacioId, AhorroDTO cuenta)
		{
			Cuenta ahorro = new Ahorro
			{
				Id = cuenta.Id,
				Nombre = cuenta.Nombre,
				Monto = cuenta.Monto,
				FechaCreacion = cuenta.FechaCreacion,
			};
			string mensaje = "";
			try
			{
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
			Cuenta credito = new Credito
			{
				Id = cuenta.Id,
				BancoEmisor = cuenta.BancoEmisor,
				NumeroTarjeta = cuenta.NumeroTarjeta,
				FechaCreacion = cuenta.FechaCreacion,
				FechaCierre = cuenta.FechaCierre,
				CreditoDisponible = cuenta.CreditoDisponible,
			};
			string mensaje = "";
			try
			{
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
			Cuenta cuenta = new Ahorro
			{
				Id = ahorroModificado.Id,
				Nombre = ahorroModificado.Nombre,
				Monto = ahorroModificado.Monto,
				FechaCreacion = ahorroModificado.FechaCreacion,
			};
			string mensaje = "";
			try
			{
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
			Cuenta cuenta = new Credito
			{
				Id = ahorroModificado.Id,
				NumeroTarjeta = ahorroModificado.NumeroTarjeta,
				BancoEmisor = ahorroModificado.BancoEmisor,
				FechaCreacion = ahorroModificado.FechaCreacion,
				FechaCierre = ahorroModificado.FechaCierre,
				CreditoDisponible = ahorroModificado.CreditoDisponible,
			};
			string mensaje = "";
			try
			{
				_espacioLogic.ModificarCuentaDeEspacio(espacioId, cuenta);
			}
			catch (DomainEspacioException e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		private TipoCambiario ConversorMonedaDTO(TipoCambiarioDTO moneda) { 
			TipoCambiario conversion = TipoCambiario.PesosUruguayos;
			if (moneda == TipoCambiarioDTO.Dolar)
			{
				conversion = TipoCambiario.Dolar;
			}
			else if (moneda == TipoCambiarioDTO.Euro)
			{
				conversion = TipoCambiario.Euro;
			}
			return conversion;
		}

		public string CrearAhorro(int espacioId, AhorroDTO ahorro) {
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
	}
}
