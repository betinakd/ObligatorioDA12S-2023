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
	}


}
