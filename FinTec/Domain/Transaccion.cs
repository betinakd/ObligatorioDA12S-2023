﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Transaccion
    {
        private string _titulo;
        public virtual string Titulo
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
        public virtual DateTime FechaTransaccion
        {
            get { return _fechaCreacion; }
        }
        private double _monto;
        public virtual double Monto
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
        public virtual TipoCambiario Moneda { get; set; }

        private Cuenta _cuenta;
        public virtual Cuenta CuentaMonetaria
        {
            get
            {
                return _cuenta;
            }
            set
            {
                if (value.Moneda != Moneda)
                    throw new DomainTransaccionException("La cuenta tiene que ser del tipo de la moneda");
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
                    throw new DomainTransaccionException("La categoria tiene que estar activa");
                _categoria = value;
            }
        }
    }
}
