using Excepcion;

namespace Domain
{
	public class TransaccionCosto : Transaccion
	{

		public override void EjecutarTransaccion()
		{
			CuentaMonetaria.EgresoMonetario(Monto);	
		}

		public override string Tipo()
		{
			return "Costo";
		}
	}
}
