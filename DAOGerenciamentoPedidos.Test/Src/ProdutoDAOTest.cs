using DAOGerenciamentoPedidos.Src;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TestesGerenciamentoPedidos
{
	[TestClass]
	public class ProdutoDAOTest
	{
		[TestMethod]
		public void AoRealizarInsercaoDeveRetonarUmNumeroInteiro()
		{
			// Arrange
			Produto produto = new Produto(null, "Produto Teste", 10.0M);
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto = 0;

			// Act
			try
			{
				idProduto = produtoDAO.Inserir(produto);

				Console.WriteLine("Produto inserido com sucesso. Id: " + idProduto);
			}
			catch (Exception e)
			{

				// Assert
				Assert.Fail("Erro ao inserir produto: " + e.Message);
			}

			// Assert
			if (idProduto <= 0)
			{
				Assert.Fail("Erro ao inserir produto");
			}
		}

		[TestMethod]
		public void AoRealizarEdicaoDeveRetonarTrue()
		{
			// Arrange
			Produto produto = new Produto(null, "Produto Teste AoRealizarEdicaoDeveRetonarTrue", 10.0M);
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto = 0;

			// Act
			try
			{
				// Inserindo produto para editar, obtendo o id
				idProduto = produtoDAO.Inserir(produto);
				produto.Descricao = "Produto Teste Editado AoRealizarEdicaoDeveRetonarTrue";
				produto.VlrUnitario = 20.0M;
				produtoDAO.Editar(produto, idProduto);
				Console.WriteLine("Produto editado com sucesso. Id: " + idProduto);
			}
			catch (Exception e)
			{

				// Assert
				Assert.Fail("Erro ao editar produto: " + e.Message);
			}

		}

		[TestMethod]
		public void AoRealizarExclusaoDeveRetonarTrue()
		{
			// Arrange
			Produto produto = new Produto(null, "Produto Teste", 10.0M);
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto = 0;

			// Act
			try
			{
				// Inserindo produto para excluir, obtendo o id
				idProduto = produtoDAO.Inserir(produto);
				produtoDAO.Excluir(idProduto);

				Console.WriteLine("Produto excluído com sucesso. Id: " + idProduto);
			}
			catch (Exception e)
			{
				// Assert
				Assert.Fail("Erro ao excluir produto: " + e.Message);
			}
		}

		[TestMethod]
		public void AoRealizarListagemDeveRetonarUmaListaDeProdutos()
		{
			// Arrange
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto1 = 0;
			int idProduto2 = 0;
			List<Produto> produtos = new List<Produto>();
			int numeroRegistrosListados = 0;

			// Act
			try
			{
				// Inserindo 2 produtos para listar
				idProduto1 = produtoDAO.Inserir(new Produto(null, "Produto Teste AoRealizarListagemDeveRetonarUmaListaDeProdutos", 10.0M));
				idProduto2 = produtoDAO.Inserir(new Produto(null, "Produto Teste 2 AoRealizarListagemDeveRetonarUmaListaDeProdutos", 20.0M));
				produtos = produtoDAO.ListarTodos();
				numeroRegistrosListados = produtos.Count;

				if (numeroRegistrosListados >= 2)
				{
					Console.WriteLine("Produtos listados com sucesso.");
					foreach (Produto produto in produtos)
					{
						Console.WriteLine("Id: " + produto.IdProduto ?? 0 + " - Descrição: " + produto.Descricao + " - Valor: " + produto.VlrUnitario);
					}

				}

			}
			catch (Exception e)
			{
				// Assert
				Assert.Fail("Erro ao listar produtos: " + e.Message);
			}

			// Assert
			numeroRegistrosListados.Should().BeGreaterThanOrEqualTo(
				2,
				because: "Ao inserir 2 produtos de teste e realizar posteriorment5 a listagem dos produtos, deve retonar uma resultado maior ou igual a 2 registros"
			);
		}

		[TestMethod]
		public void AoRealizarListagemPorDescricaoDeveRetonarUmaListaDeProdutosFiltrados()
		{
			//faz o seguinte, insere 4 produtos com a descrição testeFiltragem1, testeFiltragem2, testeFiltragem3 e testeFiltragem4. VC vai obter os ids desss registros inseridos
			//depois vc vai chamar o método de listagem por descrição passando a descrição testeFiltragem e vai verificar se a lista retornada contém os 4 registros inseridos
			// Arrange
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto1 = 0;
			int idProduto2 = 0;
			int idProduto3 = 0;
			int idProduto4 = 0;
			List<Produto> produtos = new List<Produto>();
			bool resultado = false;

			// Act
			try
			{
				// Inserindo 4 produtos para listar
				idProduto1 = produtoDAO.Inserir(new Produto(null, "testeFiltragem1", 10.0M));
				idProduto2 = produtoDAO.Inserir(new Produto(null, "testeFiltragem2", 20.0M));
				idProduto3 = produtoDAO.Inserir(new Produto(null, "testeFiltragem3", 30.0M));
				idProduto4 = produtoDAO.Inserir(new Produto(null, "testeFiltragem4", 40.0M));
				produtos = produtoDAO.ListarPorDescricao("testeFiltragem");
				resultado = produtos.Count >= 4;

				if (resultado)
				{
					Console.WriteLine("Produtos listados com sucesso.");
					foreach (Produto produto in produtos)
					{
						Console.WriteLine("Id: " + produto.IdProduto ?? 0 + " - Descrição: " + produto.Descricao + " - Valor: " + produto.VlrUnitario);
					}

				}
				else
				{
					produtos.Should().HaveCount(4);
				}

			}
			catch (Exception e)
			{
				// Assert
				e.Should().BeNull();
			}
		}
	}
}
