namespace Domain
{
	public class Ahorro : Cuenta
	{
		private string _nombre;
		private double _monto;
		public string Nombre
		{
			get
			{
				return _nombre;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new DomainCuentaException("El nombre de la cuenta no puede ser vacío");
				}
				_nombre = value;
			}
		}
		public double Monto
		{
			get
			{
				return _monto;
			}
			set
			{
				if (value <= 0)
				{
					throw new DomainCuentaException("El monto inicial de la cuenta no puede ser menor a cer.");
				}
				_monto = value;
			}
		}
		public Ahorro()
		{
		}
		public override string ToString()
		{
			return $"{base.ToString()}/n{Nombre}/n {FechaCreacion}/n {Monto}";
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Ahorro ahorro = (Ahorro) obj;
			return Nombre == ahorro.Nombre;
		}
	}
}

