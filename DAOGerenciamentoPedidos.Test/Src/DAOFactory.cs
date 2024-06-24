using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ModelsGerenciamentoPedidos.Src.Pedido;
using static ModelsGerenciamentoPedidos.Src.StatusPedido;

namespace DAOGerenciamentoPedidos.Test.Src
{
	public static class DAOFactory
	{
		public static Pedido RetornaPedido()
		{
			return new Pedido(
				idPedido: null,
				nomeCliente: $"Cliente Teste - {DateTime.Now}",
				vlrSubtotal: 100,
				desconto: 10,
				dtHrPedido: DateTime.Now,
				statusPedido: EnumStatusPedido.AguardandoPagamento,
				observacoes: $"Observações Teste - {DateTime.Now}",
				metodoPagemento: new MetodoPagamento()
				{
					IdMetodoPagto = 1
				}

			);
		}
		
	}
}
