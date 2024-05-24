﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebGereciamentoPedidos.src.dao;
using WebGereciamentoPedidos.src.models;

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
			if (idProduto <= 0)
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
				resultado = produtoDAO.editar(produto, idProduto);

				if (resultado)
					Console.WriteLine("Produto editado com sucesso. Id: " + idProduto);
			} catch (Exception e) {

				// Assert
				Assert.Fail("Erro ao editar produto: " + e.Message);
			}

			// Assert
			if (!resultado)
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
				resultado = produtoDAO.excluir(idProduto);

				if (resultado)
					Console.WriteLine("Produto excluído com sucesso. Id: " + idProduto);
			} catch (Exception e) {

				// Assert
				Assert.Fail("Erro ao excluir produto: " + e.Message);
			}

			// Assert
			if (!resultado)
			{
				Assert.Fail("Erro ao excluir produto");
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
			bool resultado = false;

			// Act
			try {
				// Inserindo 2 produtos para listar
				idProduto1 = produtoDAO.inserir(new Produto(null, "Produto Teste", 10.0M));
				idProduto2 = produtoDAO.inserir(new Produto(null, "Produto Teste 2", 20.0M));
				produtos = produtoDAO.listar();
				resultado = produtos.Count > 0;

				if (resultado)
				{
					Console.WriteLine("Produtos listados com sucesso.");
					foreach (Produto produto in produtos)
					{
						Console.WriteLine("Id: " + produto.IdProduto ?? 0 + " - Descrição: " + produto.Descricao + " - Valor: " + produto.VlrUnitario);
					}

				}

			} catch (Exception e) {

				// Assert
				Assert.Fail("Erro ao listar produtos: " + e.Message);
			}

			// Assert
			if (!resultado)
			{
				Assert.Fail("Erro ao listar produtos");
			}
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
				idProduto1 = produtoDAO.inserir(new Produto(null, "testeFiltragem1", 10.0M));
				idProduto2 = produtoDAO.inserir(new Produto(null, "testeFiltragem2", 20.0M));
				idProduto3 = produtoDAO.inserir(new Produto(null, "testeFiltragem3", 30.0M));
				idProduto4 = produtoDAO.inserir(new Produto(null, "testeFiltragem4", 40.0M));
				produtos = produtoDAO.listarPorDescricao("testeFiltragem");
				resultado = produtos.Count >= 4;

				if (resultado)
				{
					Console.WriteLine("Produtos listados com sucesso.");
					foreach (Produto produto in produtos)
					{
						Console.WriteLine("Id: " + produto.IdProduto ?? 0 + " - Descrição: " + produto.Descricao + " - Valor: " + produto.VlrUnitario);
					}

				} else {
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
