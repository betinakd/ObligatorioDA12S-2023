using System.Runtime.CompilerServices;

namespace Domain
{
	public class Credito : Cuenta
	{
		private string _bancoEmisor;
		private string _numeroTarjeta;
		private double _creditoDisponible;
		private DateTime _fechaCierre;
		public DateTime FechaCierre {
			get 
			{
				return _fechaCierre;	
			}
			set
			{
				if (value < DateTime.Now)
				{
					throw new DomainEspacioException("La fecha de cierre no puede ser menor a la fecha actual");
				}
				_fechaCierre = value;
			}
		}
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
					throw new DomainEspacioException("El banco emisor no puede ser vacío");
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
				if(!CaracterEsNumero(value))
				{
					throw new DomainEspacioException("El número de tarjeta debe ser numérico");
				}
				if (value.Length < 4)
				{
					throw new DomainEspacioException("El número de tarjeta no puede tener menos de 4 caracteres");
				}
				if (value.Length > 4)
				{
					throw new DomainEspacioException("El número de tarjeta no puede tener menos de 4 caracteres");
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
				if (value <= 0)
				{
					throw new DomainEspacioException("El crédito inicial disponible no puede ser menor a cero.");
				}
				_creditoDisponible = value;
			}
		}

		public Credito()
		{
		}

		public override void Modificar(Cuenta cuenta)
		{
			Credito credito = (Credito)cuenta;
			BancoEmisor = credito.BancoEmisor;
			NumeroTarjeta = credito.NumeroTarjeta;
			FechaCierre = credito.FechaCierre;
		}

		public override string ToString()
		{
			string baseString = base.ToString();
			return $"{baseString}{CreditoDisponible} - {NumeroTarjeta} - {BancoEmisor}";
		}
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Credito credito = (Credito)obj;
			return BancoEmisor == credito.BancoEmisor && NumeroTarjeta == credito.NumeroTarjeta;
		}

		public bool CaracterEsNumero(string palabra)
		{
			return int.TryParse(palabra, out int numero);
		}


	}

}
