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

		public virtual void IngresoMonetario(decimal monto)
		{
			throw new NotImplementedException();
		}

		public virtual void EgresoMonetario(decimal monto)
		{
			throw new NotImplementedException();
		}
	}
}