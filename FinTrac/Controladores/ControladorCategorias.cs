﻿using BussinesLogic;
using DTO;
using DTO.EnumsDTO;
using Domain;
using Excepcion;

namespace Controlador
{
	public class ControladorCategorias
	{
		private EspacioLogic _categoriaLogic;

		public ControladorCategorias(EspacioLogic categoriaLogic)
		{
			_categoriaLogic = categoriaLogic;
		}
	}
}
