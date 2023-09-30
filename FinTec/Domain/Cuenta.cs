namespace Domain
{
	public enum TipoCambiario
	{
		PesosUruguayos
	}
	public class Cuenta
	{
		public TipoCambiario Moneda { get; set; }
		public Cuenta()
		{
		}
	}
}