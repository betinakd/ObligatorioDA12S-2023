using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public class BusinessLogicEspacioException : Exception
    {
        public BusinessLogicEspacioException(string message) : base(message)
        {
        }
    }
}
