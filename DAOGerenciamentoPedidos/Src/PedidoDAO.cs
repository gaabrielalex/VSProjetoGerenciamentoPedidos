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
using System.Threading.Tasks;
using UtilsGerenciamentoPedidos;
using static ModelsGerenciamentoPedidos.Src.Pedido;

namespace DAOGerenciamentoPedidos
{
	public class PedidoDAO : IDAO<Pedido>
	{
		public PedidoDAO() { }

		public int Inserir(Pedido pedido)
		{
			// Query da inserção
			String query = @"INSERT INTO pedido (nome_cliente, vlr_subtotal, desconto, dt_hr_pedido, status_pedido, observacoes, id_metodo_pagto) 
							VALUES (@nome_cliente, @vlr_subtotal, @desconto, @dt_hr_pedido, @status_pedido, @observacoes, @id_metodo_pagto) 
							SELECT SCOPE_IDENTITY();";
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@nome_cliente", pedido.NomeCliente);
					command.Parameters.AddWithValue("@vlr_subtotal", pedido.VlrSubtotal);
					command.Parameters.AddWithValue("@desconto", pedido.Desconto);
					command.Parameters.AddWithValue("@dt_hr_pedido", pedido.DtHrPedido);
					command.Parameters.AddWithValue("@status_pedido", (char)pedido.StatusPedido);
					command.Parameters.AddWithValue("@observacoes", pedido.Observacoes);
					command.Parameters.AddWithValue("@id_metodo_pagto", pedido.MetodoPagemento.IdMetodoPagto);
					int idProduto = Convert.ToInt32(command.ExecuteScalar());
					connection.Close();
					return idProduto;

				}
			}
			catch (Exception e)
			{
				RegistroLog.Log($"Erro ao inserir pedido: {e.ToString()}");
				throw new Exception("Erro ao inserir pedido: " + e.Message);
			}
		}

		public void Editar(Pedido pedido, int idPedido)
		{
			String query = @"UPDATE pedido SET nome_cliente = @nome_cliente, vlr_subtotal = @vlr_subtotal, desconto = @desconto, dt_hr_pedido = @dt_hr_pedido, 
										status_pedido = @status_pedido, observacoes = @observacoes, id_metodo_pagto = @id_metodo_pagto WHERE id_pedido = @id_pedido";

			try
			{
				
			} catch (Exception e) {
				RegistroLog.Log($"Erro ao inserir pedido: {e.ToString()}");
				throw new Exception("Erro ao inserir pedido: " + e.Message);
			}
		}

		public void Excluir(int id)
		{
			throw new NotImplementedException();
		}

		public List<Pedido> Listar()
		{
			throw new NotImplementedException();
		}

		public Pedido ObterPorId(int id)
		{
			//Query de listagem
			String query = @"SELECT nome_cliente, vlr_subtotal, desconto, dt_hr_pedido, status_pedido, 
								observacoes, p.id_metodo_pagto, mp.descricao
							FROM pedido p, metodo_pagto mp
							WHERE p.id_metodo_pagto = mp.id_metodo_pagto 
							AND id_pedido = @id_pedido";

			List<Pedido> listaPedidos = new List<Pedido>();
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@id_pedido", id);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						Pedido pedido = new Pedido(
						idPedido: id,
						nomeCliente: reader.GetString(0),
						vlrSubtotal: reader.GetDecimal(1),
						desconto: reader.GetDecimal(2),
						dtHrPedido: reader.GetDateTime(3),
						statusPedido: (EnumStatusPedido)reader.GetString(4)[0],
						observacoes: reader.GetString(5),
						metodoPagemento: new MetodoPagamento(reader.GetInt32(6), reader.GetString(7))
						);
						listaPedidos.Add(pedido);
					}
					connection.Close();
					return listaPedidos[0];
				}
			}
			catch (Exception e)
			{
				RegistroLog.Log($"Erro ao listar produtos: {e.ToString()}");
				throw new Exception("Erro ao listar produtos: " + e.Message);
			}
		}
	}
}
