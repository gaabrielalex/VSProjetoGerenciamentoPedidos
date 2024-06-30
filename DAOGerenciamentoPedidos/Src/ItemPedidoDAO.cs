using DAOGerenciamentoPedidos.Src.Data_Base;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsGerenciamentoPedidos;

namespace DAOGerenciamentoPedidos.Src
{
	public class ItemPedidoDAO : IDAO<ItemPedido>
	{
		private readonly BancoDeDados _bancoDeDados;
		
		public ItemPedidoDAO(BancoDeDados bancoDeDados)
		{
			_bancoDeDados = bancoDeDados;
		}

		public void Editar(ItemPedido itemPedido, int idItemPedido)
		{
			var query = @"UPDATE item_pedido 
						SET qtde = @qtde, 
						vlr_total_item = @vlr_total_item, 
						id_pedido = @id_pedido, 
						id_produto = @id_produto
						WHERE id_item_pedido = @id_item_pedido";

			var parametros = new ParametroBDFactory()
								.Adicionar("@qtde", itemPedido.Quantidade)
								.Adicionar("@vlr_total_item", itemPedido.VlrTotalItem)
								.Adicionar("@id_pedido", itemPedido.IdPedido)
								.Adicionar("@id_produto", itemPedido.Produto.IdProduto)
								.Adicionar("@id_item_pedido", idItemPedido)
								.ObterParametros();
			try
			{
				var linhasAfetadas = _bancoDeDados.Executar(query, parametros);
				if (linhasAfetadas <= 0)
				{
					throw new Erro($"Erro ao editar item do pedido: Nenhuma linha foi afetada - Id: " + idItemPedido);
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao editar item do pedido: {e.ToString()}");
			}
		}

		public void Excluir(int id)
		{
			var query = "DELETE FROM item_pedido WHERE id_item_pedido = @id_item_pedido";
			var parametros = new ParametroBDFactory()
								.Adicionar("@id_item_pedido", id)
								.ObterParametros();
			try
			{
				var linhasAfetadas = _bancoDeDados.Executar(query, parametros);
				if (linhasAfetadas <= 0)
				{
					throw new Erro($"Erro ao excluir item do pedido: Nenhuma linha foi afetada - Id: " + id);
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao excluir item do pedido: {e.ToString()}");
			}
		}

		public int Inserir(ItemPedido itemPedido)
		{    
			var query = @"INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) 
						VALUES (@qtde, @vlr_total_item, @id_pedido, @id_produto)
						SELECT SCOPE_IDENTITY();";
			var parametros = new ParametroBDFactory()
								.Adicionar("@qtde", itemPedido.Quantidade)
								.Adicionar("@vlr_total_item", itemPedido.VlrTotalItem)
								.Adicionar("@id_pedido", itemPedido.IdPedido)
								.Adicionar("@id_produto", itemPedido.Produto.IdProduto)
								.ObterParametros();
			try
			{
				int idItemPedido = Convert.ToInt32(_bancoDeDados.ExecutarComRetorno(query, parametros));
				return idItemPedido;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao inserir item do pedido: {e.ToString()}");
			}
		}

		public IList<ItemPedido> ListarTodos()
		{
			var query = @"SELECT ip.*, p.*
						FROM item_pedido ip
						INNER JOIN produto p ON ip.id_produto = p.id_produto";
			try
			{
				var listaItensPedido = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query));
				return listaItensPedido;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar itens dos pedidos: {e.ToString()}");
			}
		}

		public ItemPedido ObterPorId(int itemPedido)
		{
			var query = @"SELECT ip.*, p.*
						FROM item_pedido ip
						INNER JOIN produto p ON ip.id_produto = p.id_produto
						WHERE ip.id_item_pedido = @id_item_pedido";
			var parametros = new ParametroBDFactory()
					.Adicionar("@id_item_pedido", itemPedido)
					.ObterParametros();
			try
			{
				var listaItensPedido = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros));

				if (listaItensPedido.Count == 0)
				{
					return null;
				}
				return listaItensPedido[0];
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao obter item do pedido: {e.ToString()}");
			}
		}

		public IList<ItemPedido> ListarPorIdPedido(int idPedido)
		{
			var query = @"SELECT ip.*, p.*
						FROM item_pedido ip
						INNER JOIN produto p ON ip.id_produto = p.id_produto
						WHERE ip.id_pedido = @id_pedido";

			var parametros = new ParametroBDFactory()
					.Adicionar("@id_pedido", idPedido)
					.ObterParametros();
			try
			{
				var listaItensPedido = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros));
				return listaItensPedido;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar itens dos pedidos filtrando pelo id do pedido: {e.ToString()}");
			}
		}
		public IList<ItemPedido> ConverterReaderParaListaDeObjetos(IEnumerable<IDataRecord> reader)
		{
			var listaItens = new List<ItemPedido>();

			foreach (var record in reader)
			{
				var itemPedido = new ItemPedido(
					idItemPedido: record.GetInt32(record.GetOrdinal("id_item_pedido")),
					idPedido: record.GetInt32(record.GetOrdinal("id_pedido")),
					produto: new Produto(
						idProduto: record.GetInt32(record.GetOrdinal("id_produto")),
						descricao: record.GetString(record.GetOrdinal("descricao")),
						vlrUnitario: record.GetDecimal(record.GetOrdinal("vlr_unitario"))
					),
					quantidade: record.GetInt32(record.GetOrdinal("qtde")),
					vlrTotalItem: record.GetDecimal(record.GetOrdinal("vlr_total_item"))
				);
				listaItens.Add(itemPedido);
			}

			return listaItens;
		}
	}
}
