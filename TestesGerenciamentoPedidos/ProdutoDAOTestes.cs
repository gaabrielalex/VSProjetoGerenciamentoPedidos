using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebGerenciamenttoPedidos.src.dao;
using WebGerenciamenttoPedidos.src.models;

namespace TestesGerenciamentoPedidos
{
	[TestClass]
	public class ProdutoDAOTestes
	{
		[TestMethod]
		public void AoRealizarInsercaoDeveRetonarUmNumeroInteiro()
		{
			// Arrange
			Produto produto = new Produto(null, "Produto Teste", 10.0M);
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto = 0;

			// Act
			try {
				idProduto = produtoDAO.inserir(produto);
			
				Console.WriteLine("Produto inserido com sucesso. Id: " + idProduto);
			} catch (Exception e) {
				
				// Assert
				Assert.Fail("Erro ao inserir produto: " + e.Message);
			}

			// Assert
			if(idProduto <= 0)
			{
				Assert.Fail("Erro ao inserir produto");
			}
		}

		[TestMethod]
		public void AoRealizarEdicaoDeveRetonarTrue()
		{
			// Arrange
			Produto produto = new Produto(null, "Produto Teste", 10.0M);
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto = 0;
			bool resultado = false;

			// Act
			try {
				// Inserindo produto para editar, obtendo o id
				idProduto = produtoDAO.inserir(produto);
				produto.Descricao = "Produto Teste Editado";
				produto.VlrUnitario = 20.0M;
				resultado  = produtoDAO.editar(produto, idProduto);

				if(resultado)
					Console.WriteLine("Produto editado com sucesso. Id: " + idProduto);
			} catch (Exception e) {
				
				// Assert
				Assert.Fail("Erro ao editar produto: " + e.Message);
			}

			// Assert
			if(!resultado)
			{
				Assert.Fail("Erro ao editar produto");
			}
		}

		[TestMethod]
		public void AoRealizarExclusaoDeveRetonarTrue()
		{
			// Arrange
			Produto produto = new Produto(null, "Produto Teste", 10.0M);
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto = 0;
			bool resultado = false;

			// Act
			try {
				// Inserindo produto para excluir, obtendo o id
				idProduto = produtoDAO.inserir(produto);
				resultado  = produtoDAO.excluir(idProduto);

				if(resultado)
					Console.WriteLine("Produto excluído com sucesso. Id: " + idProduto);
			} catch (Exception e) {
				
				// Assert
				Assert.Fail("Erro ao excluir produto: " + e.Message);
			}

			// Assert
			if(!resultado)
			{
				Assert.Fail("Erro ao excluir produto");
			}
		}	
	}
}
