using Excepcion;

namespace Domain
{
	public enum TipoCuenta
	{
		EsAhorro,
		EsCredito
	}
	public class Credito : Cuenta
	{
		private TipoCuenta _tipoCuenta = TipoCuenta.EsCredito;
		private string _bancoEmisor;
		private string _numeroTarjeta;
		private DateTime _fechaCierre;
		public DateTime FechaCierre
		{
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
				if (!CaracterEsNumero(value))
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

		public Credito()
		{
		}

		public override void Modificar(Cuenta cuenta)
		{
			Credito credito = (Credito)cuenta;
			FechaCierre = credito.FechaCierre;
			BancoEmisor = credito.BancoEmisor;
			NumeroTarjeta = credito.NumeroTarjeta;
		}

		public override void ModificarFecha(DateTime fecha)
		{
			FechaCierre = fecha;
		}

		public override TipoCuenta TipoDeCuenta()
		{
			return _tipoCuenta;
		}

		public override string ToString()
		{
			string baseString = base.ToString();
			return $"{baseString}{Saldo} - {NumeroTarjeta} - {BancoEmisor}";
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
