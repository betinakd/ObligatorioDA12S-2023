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
    }
}
