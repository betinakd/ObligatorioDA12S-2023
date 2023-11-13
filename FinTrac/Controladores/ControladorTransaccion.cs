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

		private Cuenta DarCuentaSegunSusDato(int espacioId, string datoCuenta)
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
			try
			{
				Cuenta cuenta = DarCuentaSegunSusDato(espacioId, transC.CuentaMonetaria);
				Categoria categoria = DarCategoriaSegunSusDato(espacioId, transC.CategoriaTransaccion);
				Transaccion transaccion = new TransaccionIngreso()
				{
					CategoriaTransaccion = categoria,
					Titulo = transC.Titulo,
					Monto = transC.Monto,
					CuentaMonetaria = cuenta,
					Moneda = cuenta.Moneda,
					
				};
				_espacioLogic.CrearTransaccion(espacioId, transaccion);
			}
			catch (DomainEspacioException e)
			{
				mensaje = e.Message;
			}
			return mensaje;
		}

		public string CrearTransaccionCosto(int espacioId, TransaccionDTO transC)
		{
			string mensaje = "";
			try
			{
				Cuenta cuenta = DarCuentaSegunSusDato(espacioId, transC.CuentaMonetaria);
				Categoria categoria = DarCategoriaSegunSusDato(espacioId, transC.CategoriaTransaccion);
				Transaccion transaccion = new TransaccionCosto()
				{
					Titulo = transC.Titulo,
					Monto = transC.Monto,
					CuentaMonetaria = cuenta,
					Moneda = cuenta.Moneda,
					CategoriaTransaccion = categoria,
				};
				_espacioLogic.CrearTransaccion(espacioId, transaccion);
			}
			catch (DomainEspacioException e)
			{
				mensaje = e.Message;
			}
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

		public List<CategoriaDTO> DarCategoriasCosto(int espacioId)
		{
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
			List<Categoria> categorias = espacio.Categorias;
			List<CategoriaDTO> categoriasCosto = new List<CategoriaDTO>();
			foreach (var c in categorias)
			{
				if (c.Tipo == TipoCategoria.Costo && c.EstadoActivo)
				{
					CategoriaDTO categoria = new CategoriaDTO()
					{
						Id = c.Id,
						Nombre = c.Nombre,
						Tipo = TipoCategoriaDTO.Costo,
						EstadoActivo = c.EstadoActivo,
						FechaCreacion = c.FechaCreacion,
					};
					categoriasCosto.Add(categoria);
				}
			}
			return categoriasCosto;
		}

		public List<CategoriaDTO> DarCategoriasIngreso(int espacioId)
		{
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
			List<Categoria> categorias = espacio.Categorias;
			List<CategoriaDTO> categoriasIngreso = new List<CategoriaDTO>();
			foreach (var c in categorias)
			{
				if (c.Tipo == TipoCategoria.Ingreso && c.EstadoActivo)
				{
					CategoriaDTO categoria = new CategoriaDTO()
					{
						Id = c.Id,
						Nombre = c.Nombre,
						Tipo = TipoCategoriaDTO.Ingreso,
						EstadoActivo = c.EstadoActivo,
						FechaCreacion = c.FechaCreacion,
					};
					categoriasIngreso.Add(categoria);
				}
			}
			return categoriasIngreso;
		}

		public TransaccionDTO DarTransaccion(int espacioId, int transaccionId)
		{
			Espacio espacio = _espacioLogic.FindEspacio(espacioId);
			List <Transaccion> transacciones = espacio.Transacciones;
			Transaccion transaccion = transacciones.Find(t => t.Id == transaccionId);
			TransaccionDTO transaccionDTO = new TransaccionDTO()
			{
				Id = transaccionId,
				CategoriaTransaccion = transaccion.CategoriaTransaccion.Nombre,
				CuentaMonetaria = transaccion.CuentaMonetaria.ToString(),
				FechaTransaccion = transaccion.FechaTransaccion,
				Moneda = ConversorMoneda(transaccion.Moneda),
				Monto = transaccion.Monto,
				Titulo = transaccion.Titulo,
			};
			return transaccionDTO;
		}

	}
}
