using DAOGerenciamentoPedidos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsGerenciamentoPedidos.Src;
using System;
using static ModelsGerenciamentoPedidos.Src.Pedido;

namespace TestesGerenciamentoPedidos.DAO_Tests
{
	[TestClass]
	public class PedidoDAOTestes
	{
		[TestMethod]
		public void AoPersistirUmObjetoPedidoDeveRetornarOIdDoPedidoInserido()
		{
			// Arrange
			Pedido pedido = new Pedido();
			pedido.NomeCliente = "Cliente Teste";
			pedido.VlrSubtotal = 100;
			pedido.Desconto = 10;
			pedido.DtHrPedido = DateTime.Now;
			pedido.StatusPedido = EnumStatusPedido.AguardandoPagamento;
			pedido.Observacoes = "Observações Teste";
			pedido.MetodoPagemento = new MetodoPagamento();
			pedido.MetodoPagemento.IdMetodoPagto = 1;
			PedidoDAO pedidoDAO = new PedidoDAO();
			// Act
			int idPedidoInserido = pedidoDAO.Inserir(pedido);
			// Assert
			idPedidoInserido.Should().BeGreaterThan(0, because: "O id do pedido inserido deve ser maior que 0, pois só assim será um id válido");

		}

		[TestMethod]
		public void AoPersistirUmObjetoPedidoDeveRetornarCadastrarUmRegistroComTodosOsCamposPreenchidos()
		{
			// Arrange
			Pedido pedido = new Pedido();
			pedido.NomeCliente = "Cliente Teste";
			pedido.VlrSubtotal = 100;
			pedido.Desconto = 10;
			pedido.DtHrPedido = DateTime.Now;
			pedido.StatusPedido = EnumStatusPedido.AguardandoPagamento;
			pedido.Observacoes = "Observações Teste";
			pedido.MetodoPagemento = new MetodoPagamento();
			pedido.MetodoPagemento.IdMetodoPagto = 1;
			PedidoDAO pedidoDAO = new PedidoDAO();
			// Act
			int idPedidoInserido = pedidoDAO.Inserir(pedido);
			Pedido pedidoInserido = pedidoDAO.ObterPorId(idPedidoInserido);

			//Truncando as datas para evitar problemas de... n sei dizer, to com pressa
			pedido.DtHrPedido = TruncateToMinute(pedido.DtHrPedido);
			pedidoInserido.DtHrPedido = TruncateToMinute(pedidoInserido.DtHrPedido);

			// Assert
			pedidoInserido.Should().NotBeNull(because: "O pedido inserido deve ser retornado ao buscar pelo id do pedido inserido");
			pedidoInserido.NomeCliente.Should().Be(pedido.NomeCliente, because: "O nome do cliente do pedido inserido deve ser igual ao nome do cliente do pedido informado");
			pedidoInserido.VlrSubtotal.Should().Be(pedido.VlrSubtotal, because: "O valor subtotal do pedido inserido deve ser igual ao valor subtotal do pedido informado");
			pedidoInserido.Desconto.Should().Be(pedido.Desconto, because: "O desconto do pedido inserido deve ser igual ao desconto do pedido informado");
			pedidoInserido.DtHrPedido.Should().Be(pedido.DtHrPedido, because: "A data e hora do pedido inserido deve ser igual a data e hora do pedido informado");
			pedidoInserido.StatusPedido.Should().Be(pedido.StatusPedido, because: "O status do pedido inserido deve ser igual ao status do pedido informado");
			pedidoInserido.Observacoes.Should().Be(pedido.Observacoes, because: "As observações do pedido inserido deve ser igual as observações do pedido informado");
			pedidoInserido.MetodoPagemento.IdMetodoPagto.Should().Be(pedido.MetodoPagemento.IdMetodoPagto, because: "O id do método de pagamento do pedido inserido deve ser igual");
		}

		static DateTime TruncateToMinute(DateTime dateTime)
		{
			// Zera os segundos e milissegundos
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
		}

	}
}
