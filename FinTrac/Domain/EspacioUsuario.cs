using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class EspacioUsuario
	{
		public int IdEspacio { get; set; }
		public string CorreoUsuario { get; set; }
		public Espacio Espacio { get; set; }
		public Usuario Usuario { get; set; }
	}
}
