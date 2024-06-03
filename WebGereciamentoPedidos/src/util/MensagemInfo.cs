using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace WebGereciamentoPedidos.src.util
{
	public class MensagemInfo
	{
		public enum TiposMensagem {
			Sucesso = 's',
			Erro = 'e',
		}

		public string Mensagem {  get; set; }
		public TiposMensagem Tipo { get; set; }
	}
}