namespace Domain
{
	public class Credito : Cuenta
	{
		private string _bancoEmisor;
		private string _numeroTarjeta;
		private double _creditoDisponible;
		public DateTime FechaCierre { get; set; }
		public string BancoEmisor
		{
			get
			{
				return _bancoEmisor;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
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
				if (value.Length > 4)
				{
					throw new DomainCuentaException("El número de tarjeta no puede tener menos de 4 caracteres");
				}
				_numeroTarjeta = value;
			}
		}
		public double CreditoDisponible
		{
			get
			{
				return _creditoDisponible;
			}
			set
			{
				if (value < 0)
				{
					throw new DomainCuentaException("El crédito disponible no puede ser negativo");
				}
				_creditoDisponible = value;
			}
		}

		public Credito()
		{
		}
		public override void IngresoMonetario(double monto)
		{
			CreditoDisponible += monto;
		}

		public override void EgresoMonetario(double monto)
		{
			CreditoDisponible -= monto;
		}

		public override string ToString()
		{
			string baseString = base.ToString();
			return $"{baseString}{CreditoDisponible}\n{NumeroTarjeta}\n{BancoEmisor}\n{FechaCierre}\n";
		}

	}
}