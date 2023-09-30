using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Transaccion
    {
        private string _titulo;
        public string Titulo
        {
            get 
            { 
                return _titulo; 
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("El titulo no puede ser vacio");
                _titulo = value;
            }
        }
        private readonly DateTime _fechaCreacion = DateTime.Now;
        public DateTime FechaTransaccion
        {
            get { return _fechaCreacion; }
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
                    throw new ArgumentException("El monto debe ser mayor a cero");
                _monto = value;
            }
        }
        public TipoCambiario Moneda { get; set; }
        public Cuenta CuentaMonetaria { get; set; }
        private Categoria _categoria;
        public Categoria CategoriaTransaccion
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (value.EstadoActivo == false)
                    throw new ArgumentException("La categoria tiene que estar activa");
                _categoria = value;
            }
        }
    }
}
