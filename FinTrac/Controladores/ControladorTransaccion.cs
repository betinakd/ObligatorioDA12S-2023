using DTO;
using BussinesLogic;
using Excepcion;
using Domain;
using DTO.EnumsDTO;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

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

		public List<string> DatosCuentasEspacio(int idEspacio) 
		{ 
			List<string> datosCuentas = new List<string>();
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
			List<Cuenta> cuentas = espacio.Cuentas;
			foreach (var c in cuentas)
			{
				string cuenta = c.ToString();
				datosCuentas.Add(cuenta);
			}
			return datosCuentas;
		}

		private Cuenta DarCuentaSegunSusDato(int espacioId , string datoCuenta)
		{ 
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
			List<Cuenta> cuentas = espacio.Cuentas;
			Cuenta cuenta = cuentas.Find(c => c.ToString().Equals(datoCuenta));
			return cuenta;
		}

		private Categoria DarCategoriaSegunSusDato(int espacioId, string datoCategoria)
		{
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
			List<Categoria> categorias = espacio.Categorias;
			Categoria categoria = categorias.Find(c => c.Nombre.Equals(datoCategoria));
			return categoria;
		}

		public string CrearTransaccionIngreso(int espacioId, TransaccionDTO transC)
		{
			string mensaje = "";

				Cuenta cuenta = DarCuentaSegunSusDato(espacioId, transC.CuentaMonetaria);
				Categoria categoria = DarCategoriaSegunSusDato(espacioId, transC.CategoriaTransaccion);
				Transaccion transaccion = new TransaccionIngreso()
				{
					Titulo = transC.Titulo,
					Monto = transC.Monto,
					CuentaMonetaria = cuenta,
					Moneda = cuenta.Moneda,
					CategoriaTransaccion = categoria,
				};
				transaccion.CuentaMonetaria.IngresoMonetario(transaccion.Monto);
				_espacioLogic.CrearTransaccion(espacioId, transaccion);

			return mensaje;
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
