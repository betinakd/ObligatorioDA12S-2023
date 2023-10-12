using Models.Excepcion;

namespace Models
{

	namespace Domain
	{
		public class TransaccionIngreso : Transaccion
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
					if (value.Tipo != TipoCategoria.Ingreso)
						throw new DomainEspacioException("La categoria tiene que ser de tipo Costo");
					_categoria = value;
				}
			}
			public override Transaccion ClonTransaccion(Transaccion transaccion)
			{
				var clon = new TransaccionIngreso()
				{
					Monto = transaccion.Monto,
					Titulo = transaccion.Titulo,
					Moneda = transaccion.Moneda,
					_categoria = transaccion.CategoriaTransaccion,
					_cuenta = transaccion.CuentaMonetaria,
					FechaTransaccion = DateTime.Today,
				};

				return clon;
			}
		}
	}
}