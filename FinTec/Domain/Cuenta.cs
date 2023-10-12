namespace Domain
{
	public enum TipoCambiario
	{
		PesosUruguayos,
		Dolar
	}
	public class Cuenta
	{
		public TipoCambiario Moneda { get; set; }
		private readonly DateTime _fechaCreacion = DateTime.Now;
		public DateTime FechaCreacion
		{
			get { return _fechaCreacion; }
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
			else
			{
				moneda = "Dolar - ";
			}
			return moneda;
		}

		public virtual void Modificar(Cuenta cuenta)
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}
	}
}