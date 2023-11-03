using Excepcion;

namespace Domain
{
	public class Transaccion
	{
		private string _titulo;
		public int Id { get; set; }
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
		public virtual Cuenta CuentaMonetaria { get; set; }
		public virtual Categoria CategoriaTransaccion { get; set; }

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

		public virtual Transaccion ClonTransaccion()
		{
			throw new DomainEspacioException("No implementado");
		}
	}
}
