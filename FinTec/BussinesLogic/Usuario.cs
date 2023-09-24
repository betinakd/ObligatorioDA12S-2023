using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public class Usuario
    {
        public string Contrasena { get; set; }

        public bool Validar_Contrasena(string contrasena)
        {   
            if(contrasena.Length>30)
            {
                return false;
            }
            return contrasena.Length>=10;
        }
    }


}
