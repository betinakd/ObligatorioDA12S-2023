using DTO;
using BussinesLogic;
using Excepcion;
using Domain;
using DTO.EnumsDTO;

namespace Controlador
{
	public class ControladorTransaccion
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;

		public ControladorTransaccion(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_espacioLogic = espacioLogic;
			_usuarioLogic = usuarioLogic;
		}

		public List<TransaccionDTO> TransaccionesDatos(int espacioId)
		{
			List<TransaccionDTO> conversionDatos = new List<TransaccionDTO>();
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
			List<Transaccion> transacciones = espacio.Transacciones;
			foreach (var t in transacciones)
			{
				TransaccionDTO transaccion = new TransaccionDTO()
				{
					Id = t.Id,
					Titulo = t.Titulo,
					FechaTransaccion = t.FechaTransaccion,
					Monto = t.Monto,
					Moneda = ConversorMoneda(t.Moneda),
					CuentaMonetaria = t.CuentaMonetaria.ToString(),
					CategoriaTransaccion = t.CategoriaTransaccion.Nombre,
					Tipo = t.Tipo(),
				};
				conversionDatos.Add(transaccion);
			}
			return conversionDatos;
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

	}
}
