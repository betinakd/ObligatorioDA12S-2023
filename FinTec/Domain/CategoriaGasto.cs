using Domain;

namespace Domain
{
    public class CategoriaGasto
    {
        private Categoria _categoria;
        private double _montoUsado;
        private int _porcentaje;

        public Categoria Categoria { get { return _categoria; } set { _categoria = value; } }
        public double MontoUsado { get { return _montoUsado; } set { _montoUsado = value;} }
        public int Porcentaje { get { return _porcentaje; } set { _porcentaje = value; } }

        public CategoriaGasto(){}

        public CategoriaGasto(Categoria c) 
        {
            _categoria = c;
            _montoUsado = 0;
            _porcentaje = 0;
        }

        public CategoriaGasto(Categoria c, double m, int p)
        {
            _categoria= c;
            _montoUsado= m;
            _porcentaje = p;
        }
    }
}
