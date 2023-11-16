using Excepcion;

namespace Dominio
{
	public class TransaccionIngreso : Transaccion
	{

		public override void EjecutarTransaccion()
		{
			CuentaMonetaria.IngresoMonetario(Monto);
		}

		public override void ModificarMonto(double nuevoMonto)
		{
			double diferencia = nuevoMonto - Monto;
			CuentaMonetaria.IngresoMonetario(diferencia);
			Monto = nuevoMonto;
		}

		public override string Tipo()
		{
			return "Ingreso";
		}
	}
}
