using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest
{
    [TestClass]
    public class TransaccionIngresoTest
    {
        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Tipo_Categoria_Distinto_TransaccionI()
        {
            var transaccion = new TransaccionIngreso();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Costo;
            categoria.EstadoActivo = true;
            transaccion.CategoriaTransaccion = categoria;
        }
    }
}
