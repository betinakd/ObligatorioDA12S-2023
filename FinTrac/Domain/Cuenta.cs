using Excepcion;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public enum TipoCambiario
	{
		PesosUruguayos,
		Dolar,
		Euro
	}
	public class Cuenta
	{
		public int Id { get; set; }
		public List<Transaccion> Transacciones { get; set; }
		public TipoCambiario Moneda { get; set; }
		private readonly DateTime _fechaCreacion = DateTime.Now;
		public DateTime FechaCreacion
		{
			get { return _fechaCreacion; }
			set { }
		}
		public Cuenta()
		{
		}

		public override string ToString()
		{
			string moneda = "";
			if (Moneda == TipoCambiario.PesosUruguayos)
			{
				moneda = "Pesos Uruguayos - ";
			}
			else if (Moneda == TipoCambiario.Dolar)
			{
				moneda = "Dolar - ";
			}
			else
			{
				moneda = "Euro - ";
			}
			return moneda;
		}

		public virtual void IngresoMonetario(double monto)
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}

		public virtual void EgresoMonetario(double monto)
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}

		public virtual void Modificar(Cuenta cuenta)
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}
	}
}