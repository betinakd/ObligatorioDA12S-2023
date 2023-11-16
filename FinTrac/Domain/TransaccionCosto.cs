using Excepcion;

namespace Dominio
{
	public class TransaccionCosto : Transaccion
	{

		public override void EjecutarTransaccion()
		{
			CuentaMonetaria.EgresoMonetario(Monto);	
		}

		public override void ModificarMonto(double nuevoMonto)
		{
			double diferencia = nuevoMonto - Monto;
			CuentaMonetaria.EgresoMonetario(diferencia);
			Monto = nuevoMonto;
		}

		public override string Tipo()
		{
			return "Costo";
		}
	}
}
