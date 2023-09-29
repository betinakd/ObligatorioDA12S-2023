using System.Security.Cryptography.X509Certificates;

namespace Domain
{
	public class Categoria
	{
		private string _nombre;
		public string Nombre
		{
			get { return _nombre; }
			set
			{
				if (value == null)
				{
					throw new DomainCategoriaException("El nombre de la categoría no puede ser vacío");
				}
				_nombre = value;
			}
		}
		public Categoria()
		{
		}
	}
}
