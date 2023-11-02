using EspacioReporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspacioReporteTest
{
	[TestClass]
	public class IngresoEgresoTest
	{
		[TestMethod]
		public void IngresoEgreso_No_Vacio() 
		{
			IngresoEgreso ingresoEgreso = new IngresoEgreso();
			Assert.IsNull(ingresoEgreso);
		}
	}
}
