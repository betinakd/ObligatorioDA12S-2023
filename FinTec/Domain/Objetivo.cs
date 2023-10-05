using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Objetivo
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
                    throw new ArgumentNullException("El titulo es requerido");
                _titulo = value;
            }
        }
        public double MontoMaximo { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}
