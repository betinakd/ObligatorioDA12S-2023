using Excepcion;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public class Transaccion
	{
		private string _titulo;
		public int Id { get; set; }
		public int CategoriaId { get; set; }
		public int CuentaId { get; set; }
		public int EspacioId { get; set; }
		public Espacio Espacio { get; set; }
		public string Titulo
		{
			get
			{
				return _titulo;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new DomainEspacioException("El titulo no puede ser vacio");
				_titulo = value;
			}
		}
		private DateTime _fechaCreacion = DateTime.Now;
		public DateTime FechaTransaccion
		{
			get
			{
				return _fechaCreacion;
			}
			set
			{
				_fechaCreacion = value;
			}
		}
		private double _monto;
		public double Monto
		{
			get
			{
				return _monto;
			}
			set
			{
				if (value <= 0)
					throw new DomainEspacioException("El monto debe ser mayor a cero");
				_monto = value;
			}
		}
		public TipoCambiario Moneda { get; set; }
		private Cuenta _cuentaMonetaria;
		public Cuenta CuentaMonetaria {

			get
			{
				return _cuentaMonetaria;
			}
			set
			{
				if(value is null)
					throw new DomainEspacioException("La cuenta monetaria no puede ser nula");
				_cuentaMonetaria = value;
			}
		}
		public Categoria CategoriaTransaccion { get; set; }

		public Cambio EncontrarCambio(Espacio espacioActual)
		{
			Cambio toRet = new Cambio();
			foreach (Cambio cambio in espacioActual.Cambios)
			{
				if (cambio.FechaDeCambio.Day == FechaTransaccion.Day && cambio.FechaDeCambio.Month == FechaTransaccion.Month
					&& cambio.FechaDeCambio.Year == FechaTransaccion.Year && cambio.Moneda == Moneda)
				{
					toRet = cambio;
				}
			}
			return toRet;
		}
		public virtual void EjecutarTransaccion()
		{
			throw new DomainEspacioException("No implementado");
		}

		public virtual string Tipo()
		{
			throw new DomainEspacioException("No implementado");
		}
	}
}
