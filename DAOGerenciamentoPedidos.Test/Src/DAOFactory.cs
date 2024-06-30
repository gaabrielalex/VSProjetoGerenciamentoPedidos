using DAOGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
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
		public static string ObterDataHoraAtualComMilisegundos()
		{
			return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
		}
		public static Produto RetornarProduto()
		{
			return new Produto()
			{
				IdProduto = null,
				Descricao = $"Produto Teste - {ObterDataHoraAtualComMilisegundos()} - {new Random().Next(-1000, 1000)} ",
				VlrUnitario = 100
			};
		}
		public static Pedido RetornarPedido()
		{
			return new Pedido(
				idPedido: null,
				nomeCliente: $"Cliente Teste - {ObterDataHoraAtualComMilisegundos()} - {new Random().Next(-1000, 1000)} ",
				vlrSubtotal: 100,
				desconto: 10,
				dtHrPedido: DateTime.Now,
				statusPedido: EnumStatusPedido.AguardandoPagamento,
				observacoes: $"Observações Teste - {ObterDataHoraAtualComMilisegundos()} - {new Random().Next(-1000, 1000)} ",
				metodoPagamento: new MetodoPagamento()
				{
					IdMetodoPagto = 1
				}

			);
		}

		public static ItemPedido RetornarItemPedido()
		{
			var idPedidoInseridoTeste = new PedidoDAO(new BancoDeDados()).Inserir(RetornarPedido());
			var idProdutoInseridoTeste = new ProdutoDAO(new BancoDeDados()).Inserir(RetornarProduto());
			var produtoInseridoTeste = new ProdutoDAO(new BancoDeDados()).ObterPorId(idProdutoInseridoTeste);
			return new ItemPedido()
			{
				IdItemPedido = null,
				IdPedido = idPedidoInseridoTeste,
				Produto = produtoInseridoTeste,
				Quantidade = new Random().Next(1, 10000),
				VlrTotalItem = new Random().Next(1, 100),
			};
		}

		public static ItemPedidoDAO ObterItemPedidoDAO()
		{
			return new ItemPedidoDAO(new BancoDeDados());
		}
		
	}
}
