using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cambio
    {
        private readonly DateTime _fechaDeCambio = DateTime.Now;
        public DateTime FechaDeCambio
        {
            get { return _fechaDeCambio; }
        }
        public TipoCambiario Moneda { get; set; }
        public double Pesos { get; set; }
    }
}
