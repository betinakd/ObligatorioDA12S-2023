using DTO;
using Domain;
using Excepcion;
using BussinesLogic;

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

				_espacioLogic.ModificarCuentaDeEspacio(espacioId, cuenta);

			return mensaje;
		}
	}
}
