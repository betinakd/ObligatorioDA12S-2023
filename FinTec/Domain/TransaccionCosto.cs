using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TransaccionCosto : Transaccion
    {
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
                if (value.Tipo != TipoCategoria.Costo)
                    throw new DomainTransaccionException("La categoria tiene que ser de tipo Ingreso");
                _categoria = value;
            }
        }
    }
}
