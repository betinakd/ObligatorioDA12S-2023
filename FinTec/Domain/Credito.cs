namespace Domain
{
	public class Credito
	{
		private string _bancoEmisor;
		private string _numeroTarjeta;
		public string BancoEmisor
		{
			get 
			{
				return _bancoEmisor;
			}
			set
			{ 
				if(string.IsNullOrEmpty(value))
				{
					throw new DomainCuentaException("El banco emisor no puede ser vacío");
				}
				_bancoEmisor = value;
			}
		}
		public string NumeroTarjeta
		{
			get
			{
				return _numeroTarjeta;
			}
			set
			{
				if (value.Length < 4)
				{
					throw new DomainCuentaException("El número de tarjeta no puede tener menos de 4 caracteres");
				}
				_numeroTarjeta = value;
			}
		}
		public Credito()
		{
		}
	}
}
