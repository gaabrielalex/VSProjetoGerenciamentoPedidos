using DAOGerenciamentoPedidos.Src.Data_Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsGerenciamentoPedidos.Src;
using System;
using UtilsGerenciamentoPedidos;

namespace DAOGerenciamentoPedidos.Test.Src
{
	[TestClass]
	public class ItemPedidoDAOTest
	{
		[TestMethod]
		public void AoPersistirUmObjetoItemPedidoDeveRetornarOIdDoPedidoInserido()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);

			idItemPedidoInserido.Should().BeGreaterThan(0, because: "O id do pedido inserido deve ser maior que 0, pois só assim será um id válido, demostrando que a inserção ocorreu com sucesso");
		}

		[TestMethod]
		public void AoInserirUmObjetoItemPedidoDeveRetornarUmRegistroComTodosOsCamposPreenchidosConformeOInserido()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);

			itemPedidoInserido.Should().NotBeNull(because: "O item do pedido inserido deve ser retornado ao buscar pelo id do item do pedido inserido");
			itemPedidoInserido.Quantidade.Should().Be(itemPedido.Quantidade, because: "A quantidade do item do pedido inserido deve ser igual a quantidade do item do pedido informado");
			itemPedidoInserido.VlrTotalItem.Should().Be(itemPedido.VlrTotalItem, because: "O valor total do item do pedido inserido deve ser igual ao valor total do item do pedido informado");
			itemPedidoInserido.IdPedido.Should().Be(itemPedido.IdPedido, because: "O id do pedido do item do pedido inserido deve ser igual ao id do pedido do item do pedido informado");
			itemPedidoInserido.Produto.IdProduto.Should().Be(itemPedido.Produto.IdProduto, because: "O id do produto do item do pedido inserido deve ser igual ao id do produto do item do pedido informado");
		}

		[TestMethod]
		public void AoInserirUmItemPedidoComIdPedidoInexistenteDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			itemPedido.IdPedido = 0;
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();

			Action action = () => itemPedidoDAO.Inserir(itemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar inserir um item do pedido com id do pedido inexistente");
		}

		[TestMethod]
		public void AoInserirUmItemPedidoComIdProdutoInexistenteDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			itemPedido.Produto.IdProduto = 0;
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();

			Action action = () => itemPedidoDAO.Inserir(itemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar inserir um item do pedido com id do produto inexistente");
		}

		[TestMethod]
		public void AoInserirUmItemPedidoComQuantidadeZeroDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			itemPedido.Quantidade = 0;
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();

			Action action = () => itemPedidoDAO.Inserir(itemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar inserir um item do pedido com quantidade zero");
		}

		[TestMethod]
		public void AoInserirUmItemPedidoComQuantidadeNegativaDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			itemPedido.Quantidade = new Random().Next(-10000, -1);
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();

			Action action = () => itemPedidoDAO.Inserir(itemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar inserir um item do pedido com quantidade zero");
		}

		[TestMethod]
		public void AoInserirUmItemPedidoComValorTotalItemZeroDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			itemPedido.VlrTotalItem = 0;
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();

			Action action = () => itemPedidoDAO.Inserir(itemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar inserir um item do pedido com valor total do item zero");
		}

		[TestMethod]
		public void AoInserirUmItemPedidoComValorTotalItemNegativoDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			itemPedido.VlrTotalItem = new Random().Next(-100, -1);
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();

			Action action = () => itemPedidoDAO.Inserir(itemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar inserir um item do pedido com valor total do item negativo");
		}

		[TestMethod]
		public void AoBuscarUmItemPedidoInexistenteDeveRetornarNulo()
		{
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var itemPedidoInexistente = itemPedidoDAO.ObterPorId(0);

			itemPedidoInexistente.Should().BeNull(because: "O item do pedido inexistente não deve ser retornado ao buscar pelo id do item do pedido inexistente");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoDeveRetornarUmRegistroComTodosOsCamposPreenchidosConformeOEditado()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);

			var idPedidoAleatorioExistenteNoBancoDeDados = DAOFactory.RetornarPedidoAleatorioExistenteNoBancoDeDados().IdPedido;
			idPedidoAleatorioExistenteNoBancoDeDados.Should().NotBe(null, because: "O id do item do pedido inserido deve ser diferente de nulo, pois só assim será um id válido, demostrando que a inserção ocorreu com sucesso");

			itemPedidoInserido.Quantidade = new Random().Next(1, 10000);
			itemPedidoInserido.VlrTotalItem = new Random().Next(1, 100);
			itemPedidoInserido.IdPedido = (int)idPedidoAleatorioExistenteNoBancoDeDados;
            itemPedidoInserido.Produto = DAOFactory.RetornarProdutoAleatorioExistenteNoBancoDeDados();

			itemPedidoDAO.Editar(itemPedidoInserido, (int)itemPedidoInserido.IdItemPedido);
			var itemPedidoEditado = itemPedidoDAO.ObterPorId((int)itemPedidoInserido.IdItemPedido);

			itemPedidoEditado.Should().NotBeNull(because: "O item do pedido editado deve ser retornado ao buscar pelo id do item do pedido editado");
			itemPedidoEditado.IdItemPedido.Should().Be(idItemPedidoInserido, because: "O id do item do pedido editado salvo no banco deve ser igual ao id do item do pedido editado informado(antes de ser salvo no banco)");
			itemPedidoEditado.VlrTotalItem.Should().Be(itemPedidoInserido.VlrTotalItem, because: "O valor total do item do pedido editado salvo no banco deve ser igual ao valor total do item do pedido editado informado(antes de ser salvo no banco)");
			itemPedidoEditado.IdPedido.Should().Be(itemPedidoInserido.IdPedido, because: "O id do pedido do item do pedido editado salvo no banco deve ser igual ao id do pedido do item do pedido editado informado(antes de ser salvo no banco)");
			itemPedidoEditado.Produto.IdProduto.Should().Be(itemPedidoInserido.Produto.IdProduto, because: "O id do produto do item do pedido editado salvo no banco deve ser igual ao id do produto do item do pedido editado informado(antes de ser salvo no banco)");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoComIdPedidoInexistenteDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);
			itemPedidoInserido.IdPedido = 0;

			Action action = () => itemPedidoDAO.Editar(itemPedidoInserido, (int)itemPedidoInserido.IdItemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar editar um item do pedido com id do pedido inexistente");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoComIdProdutoInexistenteDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);
			itemPedidoInserido.Produto.IdProduto = 0;

			Action action = () => itemPedidoDAO.Editar(itemPedidoInserido, (int)itemPedidoInserido.IdItemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar editar um item do pedido com id do produto inexistente");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoComQuantidadeZeroDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);
			itemPedidoInserido.Quantidade = 0;

			Action action = () => itemPedidoDAO.Editar(itemPedidoInserido, (int)itemPedidoInserido.IdItemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar editar um item do pedido com quantidade zero");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoComQuantidadeNegativaDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);
			itemPedidoInserido.Quantidade = new Random().Next(-10000, -1);

			Action action = () => itemPedidoDAO.Editar(itemPedidoInserido, (int)itemPedidoInserido.IdItemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar editar um item do pedido com quantidade zero");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoComValorTotalItemZeroDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);
			itemPedidoInserido.VlrTotalItem = 0;

			Action action = () => itemPedidoDAO.Editar(itemPedidoInserido, (int)itemPedidoInserido.IdItemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar editar um item do pedido com valor total do item zero");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoComValorTotalItemNegativoDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			var idItemPedidoInserido = itemPedidoDAO.Inserir(itemPedido);
			var itemPedidoInserido = itemPedidoDAO.ObterPorId(idItemPedidoInserido);
			itemPedidoInserido.VlrTotalItem = new Random().Next(-100, -1);

			Action action = () => itemPedidoDAO.Editar(itemPedidoInserido, (int)itemPedidoInserido.IdItemPedido);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar editar um item do pedido com valor total do item negativo");
		}

		[TestMethod]
		public void AoEditarUmItemPedidoInexistenteDeveRetornarErro()
		{
			var itemPedido = DAOFactory.RetornarItemPedido();
			var itemPedidoDAO = DAOFactory.ObterItemPedidoDAO();
			itemPedido.IdItemPedido = 0;

			Action action = () => itemPedidoDAO.Editar(itemPedido, 0);

			action.Should().Throw<Erro>(because: "Deve retornar erro ao tentar editar um item do pedido inexistente");
		}
	}
}
