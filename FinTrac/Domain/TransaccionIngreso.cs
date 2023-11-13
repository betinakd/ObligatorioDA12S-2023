using Excepcion;

namespace Domain
{
	public class TransaccionIngreso : Transaccion
	{
		public override Transaccion ClonTransaccion()
		{
			var clon = new TransaccionIngreso()
			{
				Monto = this.Monto,
				Titulo = this.Titulo,
				Moneda = this.Moneda,
				CategoriaTransaccion = this.CategoriaTransaccion,
				CuentaMonetaria = this.CuentaMonetaria,
				FechaTransaccion = DateTime.Today,
			};
			if (CuentaMonetaria is Ahorro)
			{
				Ahorro ahorro = (Ahorro)CuentaMonetaria;
				ahorro.IngresoMonetario(this.Monto);
			}
			if (CuentaMonetaria is Credito)
			{
				Credito credito = (Credito)CuentaMonetaria;
				credito.IngresoMonetario(this.Monto);
			}
			return clon;
		}
	}
}
