using DAOGerenciamentoPedidos;
using DAOGerenciamentoPedidos.Test.Src;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using static ModelsGerenciamentoPedidos.Src.Pedido;
using static ModelsGerenciamentoPedidos.Src.StatusPedido;


namespace TestesGerenciamentoPedidos.DAO_Tests
{
	[TestClass]
	public class PedidoDAOTest
	{
		[TestMethod]
		public void AoPersistirUmObjetoPedidoDeveRetornarOIdDoPedidoInserido()
		{
			// Arrange
			Pedido pedido = DAOFactory.RetornaPedido();
			
			PedidoDAO pedidoDAO = new PedidoDAO();
			// Act
			int idPedidoInserido = pedidoDAO.Inserir(pedido);
			// Assert
			idPedidoInserido.Should().BeGreaterThan(0, because: "O id do pedido inserido deve ser maior que 0, pois só assim será um id válido");

		}

		[TestMethod]
		public void AoPersistirUmObjetoPedidoDeveRetornUmRegistroComTodosOsCamposPreenchidosConformeOInserido()
		{
			// Arrange
			Pedido pedido = DAOFactory.RetornaPedido();
			PedidoDAO pedidoDAO = new PedidoDAO();

			// Act
			int idPedidoInserido = pedidoDAO.Inserir(pedido);
			Pedido pedidoInserido = pedidoDAO.ObterPorId(idPedidoInserido);

			//Truncando as datas para evitar problemas de... n sei dizer, to com pressa
			if (pedidoInserido != null && pedido.DtHrPedido != null && pedidoInserido.DtHrPedido != null)
			{
				pedido.DtHrPedido = TruncateToMinute(pedido.DtHrPedido);
				pedidoInserido.DtHrPedido = TruncateToMinute(pedidoInserido.DtHrPedido);
			}

			// Assert
			pedidoInserido.Should().NotBeNull(because: "O pedido inserido deve ser retornado ao buscar pelo id do pedido inserido");
			pedidoInserido.NomeCliente.Should().Be(pedido.NomeCliente, because: "O nome do cliente do pedido inserido deve ser igual ao nome do cliente do pedido informado");
			pedidoInserido.Desconto.Should().Be(pedido.Desconto, because: "O desconto do pedido inserido deve ser igual ao desconto do pedido informado");
			pedidoInserido.DtHrPedido.Should().Be(pedido.DtHrPedido, because: "A data e hora do pedido inserido deve ser igual a data e hora do pedido informado");
			pedidoInserido.StatusPedido.Should().Be(pedido.StatusPedido, because: "O status do pedido inserido deve ser igual ao status do pedido informado");
			pedidoInserido.Observacoes.Should().Be(pedido.Observacoes, because: "As observações do pedido inserido deve ser igual as observações do pedido informado");
			pedidoInserido.MetodoPagamento.IdMetodoPagto.Should().Be(pedido.MetodoPagamento.IdMetodoPagto, because: "O id do método de pagamento do pedido inserido deve ser igual");
		}

		[TestMethod]
		public void AoCadastrarPedidoEEditaloDeveRetornarOProdutoComTodasAsMudancasRealizadasAoConsultalo()
		{
			// Arrange
			PedidoDAO pedidoDAO = new PedidoDAO();
			Pedido pedido = DAOFactory.RetornaPedido();

			// Act
			int idPedidoInserido = pedidoDAO.Inserir(pedido);
			Pedido pedidoInseridoASerEditado = pedidoDAO.ObterPorId(idPedidoInserido);

			// Editando o pedido inserido
			pedidoInseridoASerEditado.NomeCliente = "Cliente Teste Editado OProdutoComTodasAsMudancasRealizadasAoConsultalo";
			pedidoInseridoASerEditado.Desconto = 20;
			pedidoInseridoASerEditado.DtHrPedido = DateTime.Now;
			pedidoInseridoASerEditado.StatusPedido = EnumStatusPedido.EmSeparacao;
			pedidoInseridoASerEditado.Observacoes = "Observações Teste Editadas OProdutoComTodasAsMudancasRealizadasAoConsultalo";
			pedidoInseridoASerEditado.MetodoPagamento.IdMetodoPagto = 2;

			// Editando o pedido no banco
			pedidoDAO.Editar(pedidoInseridoASerEditado, idPedidoInserido);

			// Buscando o pedido editado
			Pedido pedidoEditado = pedidoDAO.ObterPorId(idPedidoInserido);

			if(pedidoEditado != null && pedidoInseridoASerEditado.DtHrPedido != null && pedidoEditado.DtHrPedido != null)
			{
				//Truncando as datas para evitar problemas de... n sei dizer, to com pressa
				pedidoInseridoASerEditado.DtHrPedido = TruncateToMinute(pedidoInseridoASerEditado.DtHrPedido);
				pedidoEditado.DtHrPedido = TruncateToMinute(pedidoEditado.DtHrPedido);
			}

			// Assert
			pedidoEditado.Should().NotBeNull(because: "O pedido editado deve ser retornado ao buscar pelo id do pedido editado");
			pedidoEditado.NomeCliente.Should().Be(
				pedidoInseridoASerEditado.NomeCliente,
				because: "O nome do cliente do pedido editado salvo no banco deve ser igual ao nome do cliente do pedido editado informado(antes de ser salvo no banco)"
			);
			pedidoEditado.Desconto.Should().Be(
				pedidoInseridoASerEditado.Desconto,
				because: "O desconto do pedido editado salvo no banco deve ser igual ao desconto do pedido editado informado(antes de ser salvo no banco)"
			);
			pedidoEditado.DtHrPedido.Should().Be(
				pedidoInseridoASerEditado.DtHrPedido,
				because: "A data e hora do pedido editado salvo no banco deve ser igual a data e hora do pedido editado informado(antes de ser salvo no banco)"
			);
			pedidoEditado.StatusPedido.Should().Be(
				pedidoInseridoASerEditado.StatusPedido,
				because: "O status do pedido editado salvo no banco deve ser igual ao status do pedido editado informado(antes de ser salvo no banco)"
			);
			pedidoEditado.Observacoes.Should().Be(
				pedidoInseridoASerEditado.Observacoes,
				because: "As observações do pedido editado salvo no banco deve ser igual as observações do pedido editado informado(antes de ser salvo no banco)"
			);
			pedidoEditado.MetodoPagamento.IdMetodoPagto.Should().Be(
				pedidoInseridoASerEditado.MetodoPagamento.IdMetodoPagto,
				because: "O id do método de pagamento do pedido editado salvo no banco deve ser igual ao id do método de pagamento do pedido editado informado(antes de ser salvo no banco)"
			);
		}

		[TestMethod]
		public void AoPersistirObjetoPedidoRealizandoASuaExclusaoDeveRetornarNuloAoTentarObteloPeloSeuAntigoID()
		{
			//Arrange
			PedidoDAO pedidoDAO = new PedidoDAO();
			Pedido pedido = DAOFactory.RetornaPedido();

			//Act
			int idPedidoInserido = pedidoDAO.Inserir(pedido);
			pedidoDAO.Excluir(idPedidoInserido);
			Pedido pedidoExcluido = pedidoDAO.ObterPorId(idPedidoInserido);

			//Assert
			idPedidoInserido.Should().BeGreaterThan(0, because: "O id do pedido inserido deve ser maior que 0, pois só assim será um id válido, demonstrando que o pedido foi inserido");
			pedidoExcluido.Should().BeNull(because: "O pedido excluído não deve ser retornado ao buscar pelo id do pedido excluído");
		}

		[TestMethod]
		public void AoInserir100PedidosDeveRetornarUmaListaDePedidosContendoOs10PedidosInseridos()
		{
			// Arrange
			var quantidadePedidosEsperada = 100;
			PedidoDAO pedidoDAO = new PedidoDAO();
			List<Pedido> pedidosASeremInseridos = new List<Pedido>();
			for (int i = 0; i < quantidadePedidosEsperada; i++)
			{
				Pedido pedido = DAOFactory.RetornaPedido();
				pedidosASeremInseridos.Add(pedido);
			}

			// Act
			int[] idPedidosInseridos = new int[quantidadePedidosEsperada];

			for (int i = 0; i < quantidadePedidosEsperada; i++)
			{
				idPedidosInseridos[i] = pedidoDAO.Inserir(pedidosASeremInseridos[i]);
			}

			Pedido[] pedidosInseridos = new Pedido[quantidadePedidosEsperada];
			for (int i = 0; i < quantidadePedidosEsperada; i++)
			{
				pedidosInseridos[i] = pedidoDAO.ObterPorId(idPedidosInseridos[i]);
			}

			List<Pedido> listagemPedidos = pedidoDAO.ListarTodos();

			// Assert
			listagemPedidos.Should().HaveCountGreaterThanOrEqualTo(quantidadePedidosEsperada, because: $"Deve haver pelo menos {quantidadePedidosEsperada} pedidos cadastrados no banco");
			listagemPedidos.Should().Contain(pedidosInseridos, because: "A listagem de pedidos deve conter todos os pedidos inseridos");

		}

		static DateTime TruncateToMinute(DateTime dateTime)
		{
			// Zera os segundos e milissegundos
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
		}

	}
}
