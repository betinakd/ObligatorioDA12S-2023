using Excepcion;

namespace Dominio
{
	public class Transaccion
	{
		private string _titulo;
		private DateTime _fechaCreacion = DateTime.Now;
		private double _monto;
		private Cuenta _cuentaMonetaria;
		private Categoria _categoriaTransaccion;
		public int Id { get; set; }
		public int CategoriaId { get; set; }
		public int CuentaId { get; set; }
		public int EspacioId { get; set; }
		public Espacio Espacio { get; set; }
		public TipoCambiario Moneda { get; set; }
		public string Titulo
		{
			get
			{
				return _titulo;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new DominioEspacioExcepcion("El titulo no puede ser vacio");
				_titulo = value;
			}
		}
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
		public double Monto
		{
			get
			{
				return _monto;
			}
			set
			{
				if (value <= 0)
					throw new DominioEspacioExcepcion("El monto debe ser mayor a cero");
				_monto = value;
			}
		}
		public Cuenta CuentaMonetaria
		{
			get
			{
				return _cuentaMonetaria;
			}
			set
			{
				if (value is null)
					throw new DominioEspacioExcepcion("La cuenta monetaria no puede ser nula");
				_cuentaMonetaria = value;
			}
		}
		public Categoria CategoriaTransaccion
		{
			get
			{
				return _categoriaTransaccion;
			}
			set
			{
				if (value is null)
					throw new DominioEspacioExcepcion("La categoria no puede ser nula");
				_categoriaTransaccion = value;
			}
		}

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
			throw new DominioEspacioExcepcion("No implementado");
		}

		public virtual void ModificarMonto(double nuevoMonto)
		{
			throw new DominioEspacioExcepcion("No implementado");
		}

		public virtual string Tipo()
		{
			throw new DominioEspacioExcepcion("No implementado");
		}
	}
}
