﻿using DTO;

namespace DTOTest
{
	[TestClass]
	public class AhorroDTOTest
	{
		[TestMethod]
		public void MontoTest()
		{
			AhorroDTO ahorroDTO = new AhorroDTO();
			ahorroDTO.Monto = 1000;
			Assert.AreEqual(1000, ahorroDTO.Monto);
		}

	}
}
