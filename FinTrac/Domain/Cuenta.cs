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
		public int EspacioId { get; set; }
		public double Saldo { get; set; }
		public Espacio Espacio { get; set; }
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

		public virtual string ToString()
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

		public void IngresoMonetario(double monto)
		{
			Saldo += monto;
		}
		public void EgresoMonetario(double monto)
		{
			Saldo -= monto;
		}

		public virtual void Modificar(Cuenta cuenta)
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}

		public virtual TipoCuenta TipoDeCuenta()
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}
	}
}