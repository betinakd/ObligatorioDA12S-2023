namespace Domain
{
	public class Credito
	{
		private string _bancoEmisor;
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
		public Credito()
		{
		}
	}
}
