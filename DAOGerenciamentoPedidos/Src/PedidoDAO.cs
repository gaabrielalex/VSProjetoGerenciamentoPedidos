using DAOGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtilsGerenciamentoPedidos;
using static ModelsGerenciamentoPedidos.Src.Pedido;
using static ModelsGerenciamentoPedidos.Src.StatusPedido;

namespace DAOGerenciamentoPedidos
{
	public class PedidoDAO : IDAO<Pedido>
	{
		private readonly BancoDeDados _bancoDeDados;
		
		public PedidoDAO(BancoDeDados bancoDeDados)
		{
			_bancoDeDados = bancoDeDados;
		}

		public int Inserir(Pedido pedido)
		{
			var query = @"INSERT INTO pedido (nome_cliente, desconto, dt_hr_pedido, status_pedido, observacoes, id_metodo_pagto) 
							VALUES (@nome_cliente, @desconto, @dt_hr_pedido, @status_pedido, @observacoes, @id_metodo_pagto) 
							SELECT SCOPE_IDENTITY();";
			var parametros = new ParametroBDFactory()
								.Adicionar("@nome_cliente", pedido.NomeCliente)
								.Adicionar("@desconto", pedido.Desconto)
								.Adicionar("@dt_hr_pedido", pedido.DtHrPedido)
								.Adicionar("@status_pedido", (char)pedido.StatusPedido)
								.Adicionar("@observacoes", pedido.Observacoes)
								.Adicionar("@id_metodo_pagto", pedido.MetodoPagamento.IdMetodoPagto)
								.ObterParametros();
			try
			{
				int idPedido = Convert.ToInt32(_bancoDeDados.ExecutarComRetorno(query, parametros));
				return idPedido;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao inserir pedido: {e.ToString()}");
			}
		}

		public void Editar(Pedido pedido, int idPedido)
		{
			var query = @"UPDATE pedido SET nome_cliente = @nome_cliente, desconto = @desconto, dt_hr_pedido = @dt_hr_pedido, 
										status_pedido = @status_pedido, observacoes = @observacoes, id_metodo_pagto = @id_metodo_pagto WHERE id_pedido = @id_pedido";
			var parametros = new ParametroBDFactory()
					.Adicionar("@nome_cliente", pedido.NomeCliente)
					.Adicionar("@desconto", pedido.Desconto)
					.Adicionar("@dt_hr_pedido", pedido.DtHrPedido)
					.Adicionar("@status_pedido", (char)pedido.StatusPedido)
					.Adicionar("@observacoes", pedido.Observacoes)
					.Adicionar("@id_metodo_pagto", pedido.MetodoPagamento.IdMetodoPagto)
					.Adicionar("@id_pedido", idPedido)
					.ObterParametros();
			try
			{
				var linhasAfetadas = _bancoDeDados.Executar(query, parametros);
				if (linhasAfetadas < 0)
				{
					throw new Erro("Erro ao editar pedido: Nenhuma linha foi afetada");
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao editar pedido: {e.ToString()}");
			}
		}

		public void Excluir(int id)
		{
			var query = "DELETE FROM pedido WHERE id_pedido = @id_pedido";
			var parametros = new ParametroBDFactory()
					.Adicionar("@id_pedido", id)
					.ObterParametros();
			try
			{
				var linhasAfetadas = _bancoDeDados.Executar(query, parametros);
				if (linhasAfetadas < 0)
				{
					throw new Erro($"Erro ao excluir pedido: Nenhuma linha foi afetada - Id: " + id);
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao excluir pedido: {e.ToString()}");
			}
		}

		public IList<Pedido> ListarTodos()
		{
			var query = @"SELECT p.*, 
								mp.descricao AS descricao_metodo_pagto,
								COALESCE(SUM(ip.vlr_total_item), 0) AS vlr_subtotal
							FROM pedido p
							LEFT JOIN metodo_pagto mp ON p.id_metodo_pagto = mp.id_metodo_pagto
							LEFT JOIN item_pedido ip ON p.id_pedido = ip.id_pedido
							GROUP BY p.id_pedido, p.dt_hr_pedido, p.nome_cliente, 
								p.desconto, p.status_pedido, p.observacoes, 
								p.id_metodo_pagto, mp.descricao
							ORDER BY p.dt_hr_pedido DESC;";

			try
			{
				var listaPedido = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query));
				return listaPedido;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar pedidos: {e.ToString()}");
			}
		}

		public Pedido ObterPorId(int id)
		{
			var query =  @"SELECT p.*, 
								mp.descricao AS descricao_metodo_pagto,
								COALESCE(SUM(ip.vlr_total_item), 0) AS vlr_subtotal
							FROM pedido p
							LEFT JOIN metodo_pagto mp ON p.id_metodo_pagto = mp.id_metodo_pagto
							LEFT JOIN item_pedido ip ON p.id_pedido = ip.id_pedido
							WHERE p.id_pedido = @id_pedido
							GROUP BY p.id_pedido, p.dt_hr_pedido, p.nome_cliente, 
								p.desconto, p.status_pedido, p.observacoes, 
								p.id_metodo_pagto, mp.descricao;";
			var parametros = new ParametroBDFactory()
					.Adicionar("@id_pedido", id)
					.ObterParametros();
			try
			{
				var listaPedidos = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros));

				if (listaPedidos.Count == 0)
				{
					return null;
				}
				return listaPedidos[0];
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao obter pedido: {e.ToString()}");
			}
		}

		public IList<Pedido> ListarPorCliente(string nomeCliente)
		{
			var query = @"SELECT p.*, 
								mp.descricao AS descricao_metodo_pagto,
								COALESCE(SUM(ip.vlr_total_item), 0) AS vlr_subtotal
							FROM pedido p
							LEFT JOIN metodo_pagto mp ON p.id_metodo_pagto = mp.id_metodo_pagto
							LEFT JOIN item_pedido ip ON p.id_pedido = ip.id_pedido
								WHERE nome_cliente COLLATE Latin1_General_CI_AI LIKE @nome_cliente
							GROUP BY p.id_pedido, p.dt_hr_pedido, p.nome_cliente, 
								p.desconto, p.status_pedido, p.observacoes, 
								p.id_metodo_pagto, mp.descricao
							ORDER BY p.dt_hr_pedido DESC;";
			var parametros = new ParametroBDFactory()
					.Adicionar("@nome_cliente", "%" + nomeCliente + "%")
					.ObterParametros();
			try
			{
				var listaPedidos = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query, parametros));
				return listaPedidos;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar pedidos por cliente: {e.ToString()}");
			}
		}

		public IList<Pedido> ConverterReaderParaListaDeObjetos(SqlDataReader reader)
		{
			var listaPedidos = new List<Pedido>();

			// Obtendo os índices das colunas uma vez antes do loop
			int idIndex = reader.GetOrdinal("id_pedido");
			int nomeClienteIndex = reader.GetOrdinal("nome_cliente");
			int vlrSubtotalIndex = reader.GetOrdinal("vlr_subtotal");
			int descontoIndex = reader.GetOrdinal("desconto");
			int dtHrPedidoIndex = reader.GetOrdinal("dt_hr_pedido");
			int statusPedidoIndex = reader.GetOrdinal("status_pedido");
			int observacoesIndex = reader.GetOrdinal("observacoes");
			int idMetodoPagtoIndex = reader.GetOrdinal("id_metodo_pagto");
			int descricaoMetodoPagtoIndex = reader.GetOrdinal("descricao_metodo_pagto");

			while (reader.Read())
			{
				Pedido pedido = new Pedido(
					idPedido: reader.GetInt32(idIndex),
					nomeCliente: reader.GetString(nomeClienteIndex),
					vlrSubtotal: reader.GetDecimal(vlrSubtotalIndex),
					desconto: reader.GetDecimal(descontoIndex),
					dtHrPedido: reader.GetDateTime(dtHrPedidoIndex),
					statusPedido: (EnumStatusPedido)reader.GetString(statusPedidoIndex)[0],
					observacoes: reader.IsDBNull(observacoesIndex) ? "" : reader.GetString(observacoesIndex),
					metodoPagamento: new MetodoPagamento(reader.GetInt32(idMetodoPagtoIndex), reader.GetString(descricaoMetodoPagtoIndex))
				);
				listaPedidos.Add(pedido);
			}

			return listaPedidos;
		}
	}
}
