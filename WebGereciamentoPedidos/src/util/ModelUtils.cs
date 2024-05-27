using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGereciamentoPedidos.src.util
{
	public class ModelUtils
	{
		public static int ObterQuantidadeDeDigitosAntesDoSeparadorDecimal(decimal valor)
		{
			var quantidadeDigitosAntesDoSeparadorDecimal = 0;
			string valorString = valor.ToString();
			if(valorString.Contains("."))
			{
				quantidadeDigitosAntesDoSeparadorDecimal = valorString.Split('.')[0].Length;
			}
			else if (valorString.Contains(","))
			{
				quantidadeDigitosAntesDoSeparadorDecimal = valorString.Split(',')[0].Length;
			}
			else
			{
				quantidadeDigitosAntesDoSeparadorDecimal = valorString.Length;
			}
			return quantidadeDigitosAntesDoSeparadorDecimal;
			
		}
	}
}