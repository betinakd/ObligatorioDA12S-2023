using Domain;

namespace EspacioReporte
{
	public class IngresoEgreso
	{
		private DateTime _fecha;
		private double _ingresos;
		private double _esgresos;

		public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public double Ingresos { get { return _ingresos; } set { _ingresos = value; } }
		public double Egresos { get { return _esgresos; } set { _esgresos = value; } }

		public IngresoEgreso() 
		{ 
			Fecha = DateTime.Now;
		}

		public IngresoEgreso(DateTime fecha, double ingresos, double egresos)
		{
			Fecha = fecha;
			Ingresos = ingresos;
			Egresos = egresos;
		}
	}
}