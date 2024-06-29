using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ModelsGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
using UtilsGerenciamentoPedidos;
using System.Runtime.Remoting.Messaging;

namespace DAOGerenciamentoPedidos.Src
{
	public class ProdutoDAO : IDAO<Produto>
	{
		private readonly BancoDeDados _bancoDeDados;

		public ProdutoDAO(BancoDeDados bancoDeDados)
		{
			_bancoDeDados = bancoDeDados;
		}

		public int Inserir(Produto produto)
		{
			var query = @"INSERT INTO produto (descricao, vlr_unitario) VALUES (@descricao, @vlr_unitario);
							SELECT SCOPE_IDENTITY();";
			var parametros = new ParametroBDFactory()
								.Adicionar("@descricao", produto.Descricao)
								.Adicionar("@vlr_unitario", produto.VlrUnitario)
								.ObterParametros();
			try
			{
				int idProduto = Convert.ToInt32(_bancoDeDados.ExecutarComRetorno(query, parametros));
				return idProduto;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao inserir produto: {e.ToString()}");
			}
		}

		public void Editar(Produto produto, int id)
		{
			var query = "UPDATE produto SET descricao = @descricao, vlr_unitario = @vlr_unitario WHERE id_produto = @id_produto";
			var parametros = new ParametroBDFactory()
								.Adicionar("@descricao", produto.Descricao)
								.Adicionar("@vlr_unitario", produto.VlrUnitario)
								.Adicionar("@id_produto", id)
								.ObterParametros();
			try
			{
				var linhasAfetadas = _bancoDeDados.Executar(query, parametros);
				if (linhasAfetadas < 0)
				{
					throw new Erro($"Erro ao editar produto: Nenhuma linha foi afetada - Id: " + id);
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao editar produto: {e.ToString()}");
			}
		}

		public void Excluir(int id)
		{
			var query = "DELETE FROM produto WHERE id_produto = @id_produto";
			var parametros = new ParametroBDFactory()
								.Adicionar("@id_produto", id)
								.ObterParametros();
			try
			{
				var linhasAfetadas = _bancoDeDados.Executar(query, parametros);
				if (linhasAfetadas < 0)
				{
					throw new Erro($"Erro ao excluir produto: Nenhuma linha foi afetada - Id: " + id);
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao excluir produto: {e.ToString()}");
			}
		}

		public List<Produto> ListarTodos()
		{
			var query = "SELECT * FROM produto ORDER BY descricao";
			try
			{
				var listaProdutos = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query));
				return listaProdutos;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar produtos: {e.ToString()}");
			}
		}

		public List<Produto> ListarPorDescricao(String descricao)
		{
			var query = @"SELECT * 
							FROM produto 
							WHERE descricao COLLATE Latin1_General_CI_AI LIKE @descricao 
							ORDER BY descricao";
			var parametros = new ParametroBDFactory()
					.Adicionar("@descricao", "%" + descricao + "%")
					.ObterParametros();
			try
			{
				var listaProdutos = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query,parametros));
				return listaProdutos;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar produtos: {e.ToString()}");
			}
		}

		public Produto ObterPorId(int id)
		{
			var query = "SELECT * FROM produto where id_produto = @id_produto";
			var parametros = new ParametroBDFactory()
					.Adicionar("@id_produto", id)
					.ObterParametros();
			try
			{
				var listaProdutos = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros));
				if (listaProdutos.Count == 0)
				{
					return null;
				}
				return listaProdutos[0];
				
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao obter produto: {e.ToString()}");
			}
		}

		public bool DescricaoJaExiste(String descricao)
		{
			var query = "SELECT * FROM produto WHERE descricao COLLATE Latin1_General_CI_AI = @descricao";
			var parametros = new ParametroBDFactory()
					.Adicionar("@descricao", descricao)
					.ObterParametros();
			try
			{
				var reader = _bancoDeDados.ConsultarReader(query, parametros);
				if(reader.HasRows)
				{
					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao realizar verificação da já existência do produto: {e.ToString()}");
			}
		}

		public List<Produto> ConverterReaderParaListaDeObjetos(SqlDataReader reader)
		{
			var listaProdutos = new List<Produto>();

			// Obtendo os índices das colunas uma vez antes do loop
			int idIndex = reader.GetOrdinal("id_produto");
			int descricaoIndex = reader.GetOrdinal("descricao");
			int vlrUnitarioIndex = reader.GetOrdinal("vlr_unitario");

			while (reader.Read())
			{
				Produto produto = new Produto(
					idProduto: reader.GetInt32(idIndex),
					descricao: reader.GetString(descricaoIndex),
					vlrUnitario: reader.GetDecimal(vlrUnitarioIndex)
				);
				listaProdutos.Add(produto);
			}

			return listaProdutos;
		}
	}
}