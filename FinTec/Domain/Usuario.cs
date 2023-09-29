using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public class Usuario
    {
        public string Contrasena { get; set; }
        public string Correo { get; set; }

        private const string _patron = @".+\.com$";

        public bool Validar_Contrasena(string contrasena)
        {
            if (EsContrasenaMayorATreinta(contrasena))
            {
                return false;
            }

            if (SonTodasMinusculas(contrasena))
            {
                return false;
            } 

            return EsContrasenaMayorIgualADiez(contrasena);
        }
        public bool Validar_Correo(string correo)
        {
            if (!ContienePuntoCom(correo))
            {
                return false;
            }
            return ContieneArroba(correo);
        }

        private static bool ContienePuntoCom(string correo)
        {
            return Regex.IsMatch(correo, _patron);
        }

        private static bool ContieneArroba(string correo)
        {
            return correo.Contains("@");
        }

        private static bool SonTodasMinusculas(string contrasena)
        {
            return contrasena.All(char.IsLower);
        }

        private static bool EsContrasenaMayorIgualADiez(string contrasena)
        {
            return contrasena.Length >= 10;
        }

        private static bool EsContrasenaMayorATreinta(string contrasena)
        {
            return contrasena.Length > 30;
        }
    }


}
