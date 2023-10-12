using Domain;

namespace EspacioReporte
{
    public class CategoriaGasto
    {
        private Categoria _categoria;
        private double _montoUsado;
        private double _porcentaje;

        public Categoria Categoria { get { return _categoria; } set { _categoria = value; } }
        public double MontoUsado { get { return _montoUsado; } set { _montoUsado = value;} }
        public double Porcentaje { get { return _porcentaje; } set { _porcentaje = value; } }

        public CategoriaGasto(){}

        public CategoriaGasto(Categoria c) 
        {
            _categoria = c;
            _montoUsado = 0;
            _porcentaje = 0;
        }

        public CategoriaGasto(Categoria c, double m, double p)
        {
            _categoria= c;
            _montoUsado= m;
            _porcentaje = p;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            };

            CategoriaGasto og = (CategoriaGasto)obj;
            return Categoria.Equals(og.Categoria) && MontoUsado == og.MontoUsado && Porcentaje == og.Porcentaje;
        }
    }
}
