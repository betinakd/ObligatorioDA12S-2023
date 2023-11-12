using Excepcion;

namespace Domain
{
	public class TransaccionCosto : Transaccion
	{
		public override Transaccion ClonTransaccion()
		{
			var clon = new TransaccionCosto()
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
				ahorro.EgresoMonetario(this.Monto);
			}
			if (CuentaMonetaria is Credito)
			{
				Credito credito = (Credito)CuentaMonetaria;
				credito.EgresoMonetario(this.Monto);
			}
			return clon;
		}

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
