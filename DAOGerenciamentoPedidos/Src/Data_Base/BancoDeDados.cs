using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsGerenciamentoPedidos;

namespace DAOGerenciamentoPedidos.Src.Data_Base
{
	public class BancoDeDados
	{
		public static SqlConnection CriarConexao()
		{
			//Da erro
			//var connectionString = 
			//	ConfigurationManager
			//		.ConnectionStrings["ConexaoBD"]
			//			.ConnectionString;

			//return new SqlConnection(connectionString);

			//Pegando a string de conexao direto da certo
			return new SqlConnection("Data Source=GABRIELSILVA\\SQLEXPRESS;Initial Catalog=DB_Gerenciamento_Pedidos;Integrated Security=True;Encrypt=False;TrustServerCertificate=True");
		}

		public IEnumerable<IDataRecord> ConsultarReader(string sql)
		{
			return ConsultarReader(sql, null);
		}

		public IEnumerable<IDataRecord> ConsultarReader(string sql, List<ParametroBD> parametros)
		{
			var conexao = CriarConexao();
			var comando = conexao.CreateCommand();
			AdicionarParametrosAQuery(comando, parametros);
			comando.CommandText = sql;
			conexao.Open();
			using (var reader = comando.ExecuteReader(CommandBehavior.CloseConnection))
			{
				while (reader.Read())
				{
					yield return reader;
				}
			}
		}

		public int Executar(string sql, List<ParametroBD> parametros, bool EhProcedure = false)
		{
			using (var conexao = CriarConexao())
			using (var comando = conexao.CreateCommand())
			{
				AdicionarParametrosAQuery(comando, parametros);

				if (EhProcedure)
					comando.CommandType = CommandType.StoredProcedure;

				comando.CommandText = sql;

				conexao.Open();
				try
				{
					return comando.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw e;
				}
				finally
				{
					conexao.Close();
				}
			}
		}

		public object ExecutarComRetorno(string query, List<ParametroBD> parametros)
		{
			using (var conexao = BancoDeDados.CriarConexao())
			using (var comando = conexao.CreateCommand())
			{
				AdicionarParametrosAQuery(comando, parametros);
				comando.CommandText = query;
				conexao.Open();
				try
				{
					return comando.ExecuteScalar();
				}
				catch (Exception e)
				{
					throw e;
				}
				finally
				{
					conexao.Close();
				}
			}
		}

		public int ExecutarComTransacao(string sql, List<ParametroBD> parametros, bool EhProcedure = false)
		{
			using (var conexao = CriarConexao())
			using (var comando = conexao.CreateCommand())
			{
				AdicionarParametrosAQuery(comando, parametros);

				if (EhProcedure)
					comando.CommandType = CommandType.StoredProcedure;

				comando.CommandText = sql;

				conexao.Open();
				var transacao = conexao.BeginTransaction();
				comando.Transaction = transacao;
				try
				{
					int linhasAfetadas = comando.ExecuteNonQuery();
					transacao.Commit();
					return linhasAfetadas;
				}
				catch (Exception e)
				{
					transacao.Rollback();
					throw e;
				}
				finally
				{
					conexao.Close();
				}
			}
		}

		public void AdicionarParametrosAQuery(SqlCommand sqlCommand, List<ParametroBD> parametros)
		{
			if (parametros == null)
				return;
			for (int n = 0; n < parametros.Count; n++)
			{
				var filtro = sqlCommand.CreateParameter();
				filtro.ParameterName = parametros[n].Nome;
				filtro.Value = parametros[n].Valor;
				sqlCommand.Parameters.Add(filtro);
			}
		}
	}
}