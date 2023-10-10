namespace Domain
{
    public class Transaccion
    {
        public static int _contadorIdTransaccion = 1;
        private string _titulo;
        public int IdTransaccion { get; set; }
        public string Titulo
        {
            get 
            { 
                return _titulo; 
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new DomainEspacioException("El titulo no puede ser vacio");
                _titulo = value;
            }
        }
        private  DateTime _fechaCreacion = DateTime.Now;
        public DateTime FechaTransaccion
        {
            get
            {
                return _fechaCreacion;
            }
            set 
            {
                _fechaCreacion = value;
            }
        }
        private double _monto;
        public double Monto
        {
            get
            {
                return _monto;
            }
            set
            {
                if (value <= 0)
                    throw new DomainEspacioException("El monto debe ser mayor a cero");
                _monto = value;
            }
        }
        public TipoCambiario Moneda { get; set; }

        private Cuenta _cuenta;
        public Cuenta CuentaMonetaria
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
        public virtual Categoria CategoriaTransaccion
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (value.EstadoActivo == false)
                    throw new DomainEspacioException("La categoria tiene que estar activa");
                _categoria = value;
            }
        }

        public static void AumentarContadorIdTransaccion()
        {
             _contadorIdTransaccion++;
        }

        public void AsignarIdTransaccion()
        {
            IdTransaccion = _contadorIdTransaccion;
            AumentarContadorIdTransaccion();
        }
    }
}
