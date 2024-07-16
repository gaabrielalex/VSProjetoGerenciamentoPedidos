using DAOGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DAOGerenciamentoPedidos.Test.Src
{
	[TestClass]
	public class MetodoPagamentoDAOTest
	{
		[TestMethod]
		public void AoObterTodosMetodosPagamentoDeveRetornarUmaListaComNoMinimoCincoMetodosPagamento()
		{
			// Arrange
			MetodoPagamentoDAO metodoPagamentoDAO = new MetodoPagamentoDAO(new BancoDeDados());

			// Act
			var metodosPagamento = metodoPagamentoDAO.ListarTodos();

			// Assert
			metodosPagamento.Should().HaveCountGreaterOrEqualTo(5, because: "deve haver no mínimo 5 métodos de pagamento cadastrados, pois são os métodos padrões cadastrados no sistema");
		}
	}
}
