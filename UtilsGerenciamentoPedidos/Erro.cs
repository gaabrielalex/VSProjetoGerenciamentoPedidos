using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsGerenciamentoPedidos
{
	public class Erro : Exception
	{
		public Erro(string mensagem) : base(mensagem)
		{
			RegistroLog.Log(mensagem);
		}
	}
}
