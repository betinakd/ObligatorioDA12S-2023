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

		public virtual void IngresoMonetario(double monto)
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}

		public virtual void EgresoMonetario(double monto)
		{
			throw new NotImplementedException("Esta operación no esta disponible en esta Cuenta");
		}

		public override string ToString()
		{
			string fecha = DateTime.Now.ToString();
			string moneda = "";
			if (Moneda == TipoCambiario.PesosUruguayos)
			{
				moneda = "Pesos Uruguayos";
			}
			else
			{
				moneda = "Dolar";
			}
			return moneda + "\n" + fecha + "\n";
		}
	}
}