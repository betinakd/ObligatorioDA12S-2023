using Excepcion;

namespace Domain
{
	public class TransaccionCosto : Transaccion
	{
		private Cuenta _cuenta;
		public override Cuenta CuentaMonetaria
		{
			get
			{
				return _cuenta;
			}
			set
			{
				if (value == null)
					throw new DomainEspacioException("La cuenta no puede ser nula");
				if (value.Moneda != Moneda)
					throw new DomainEspacioException("La cuenta tiene que ser del tipo de la moneda");
				_cuenta = value;
			}
		}
		private Categoria _categoria;
		public override Categoria CategoriaTransaccion
		{
			get
			{
				return _categoria;
			}
			set
			{
				if (value == null)
					throw new DomainEspacioException("La categoria no puede ser nula");
				if (value.EstadoActivo == false)
					throw new DomainEspacioException("La categoria tiene que estar activa");
				if (value.Tipo != TipoCategoria.Costo)
					throw new DomainEspacioException("La categoria tiene que ser de tipo Costo");
				_categoria = value;
			}
		}
		public override Transaccion ClonTransaccion(Transaccion transaccion)
		{
			var clon = new TransaccionCosto()
			{
				Monto = transaccion.Monto,
				Titulo = transaccion.Titulo,
				Moneda = transaccion.Moneda,
				_categoria = transaccion.CategoriaTransaccion,
				_cuenta = transaccion.CuentaMonetaria,
				FechaTransaccion = DateTime.Today,
			};
			if (_cuenta is Ahorro)
			{
				Ahorro ahorro = (Ahorro)_cuenta;
				ahorro.EgresoMonetario(transaccion.Monto);
			}
			if (_cuenta is Credito)
			{
				Credito credito = (Credito)_cuenta;
				credito.EgresoMonetario(transaccion.Monto);
			}
			return clon;
		}
	}
}
