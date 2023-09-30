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
		public Cuenta()
		{
		}
	}
}