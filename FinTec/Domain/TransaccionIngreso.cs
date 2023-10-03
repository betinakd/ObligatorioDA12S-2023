using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TransaccionIngreso : Transaccion
    {
        private string _titulo;
        public override string Titulo
        {
            get
            {
                return _titulo;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new DomainTransaccionException("El titulo no puede ser vacio");
                _titulo = value;
            }
        }

        private readonly DateTime _fechaCreacion = DateTime.Now;
        public override DateTime FechaTransaccion
        {
            get { return _fechaCreacion; }
        }

        private double _monto;
        public override double Monto
        {
            get
            {
                return _monto;
            }
            set
            {
                if (value <= 0)
                    throw new DomainTransaccionException("El monto debe ser mayor a cero");
                _monto = value;
            }
        }

        public override TipoCambiario Moneda { get; set; }

        public override Cuenta CuentaMonetaria { get; set; }

        private Categoria _categoria;
        public override Categoria CategoriaTransaccion
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (value.EstadoActivo == false)
                    throw new DomainTransaccionException("La categoria tiene que estar activa");
                _categoria = value;
            }
        }
    }
}
