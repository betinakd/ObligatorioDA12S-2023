namespace Domain
{
	public class Ahorro : Cuenta
	{
		private string _nombre;
		public string Nombre 
		{
			get
			{ 
				return _nombre;
			}
			set
			{ 
				if(string.IsNullOrEmpty(value))
				{
					throw new DomainCuentaException("El nombre de la cuenta no puede ser vacío");
				}
				_nombre = value;
			}
		}
		public double Monto { get; set; }
		public Ahorro()
		{
		}
	}
}

