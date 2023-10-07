using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Cambio
	{
		private DateTime _fechaDeCambio = DateTime.Now.Date;
		private double _pesos;
		public DateTime FechaDeCambio
		{
			get { return _fechaDeCambio; }
			set { _fechaDeCambio = value; }
		}
		public TipoCambiario Moneda { get; set; }
		public double Pesos
		{
			get
			{
				return _pesos;
			}
			set
			{
				if (value > 0)
					throw new Exception("El monto en pesos uruguayos debe ser mayor a 0");
				_pesos = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Cambio cambio = (Cambio)obj;
			return FechaDeCambio.Date == cambio.FechaDeCambio.Date;
		}
	}
}
