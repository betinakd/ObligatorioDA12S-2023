using Excepcion;

namespace Domain
{
	public class TransaccionIngreso : Transaccion
	{

		public override void EjecutarTransaccion()
		{
			CuentaMonetaria.IngresoMonetario(Monto);
		}

		public override string Tipo()
		{
			return "Ingreso";
		}
	}
}
