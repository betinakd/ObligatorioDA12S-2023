using Excepcion;

namespace Dominio
{
	public class Ahorro : Cuenta
	{
		private TipoCuenta _tipoCuenta = TipoCuenta.EsAhorro;
		private string _nombre;
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
					throw new DominioEspacioExcepcion("El nombre de la cuenta no puede ser vacío");
				}
				_nombre = value;
			}
		}
		public Ahorro()
		{
		}


		public override void Modificar(Cuenta cuenta)
		{
			Ahorro ahorro = (Ahorro)cuenta;
			Nombre = ahorro.Nombre;
		}
		public override TipoCuenta TipoDeCuenta()
		{
			return _tipoCuenta;
		}

		public override string ToString()
		{
			return $"{base.ToString()}{Nombre} - {Saldo}";
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Ahorro ahorro = (Ahorro)obj;
			return Nombre == ahorro.Nombre;
		}
	}
}

