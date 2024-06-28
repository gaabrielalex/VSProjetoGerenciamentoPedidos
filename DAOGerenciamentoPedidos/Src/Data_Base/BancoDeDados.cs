using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsGerenciamentoPedidos;

namespace DAOGerenciamentoPedidos.Src.Data_Base
{
	public class BancoDeDados
	{
		//Esse método não deve ser static
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

		protected DataTable Consultar(string sql)
		{
			return Consultar(sql, null);
		}

		protected DataTable Consultar(string sql, List<ParametroBD> parametros)
		{
			using (var conexao = CriarConexao())
			using (var comando = conexao.CreateCommand())
			{
				AdicionarParametrosAQuery(comando, parametros);

				comando.CommandText = sql;

				using (var adapter = new SqlDataAdapter(comando))
				{
					var tabela = new DataTable();
					adapter.Fill(tabela);
					return tabela;
				}
			}
		}

		public int ExecutarComRetorno(string query, List<ParametroBD> parametros)
		{
			using (SqlConnection connection = BancoDeDados.CriarConexao())
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				AdicionarParametrosAQuery(command, parametros);
				connection.Open();
				try
				{
					return Convert.ToInt32(command.ExecuteScalar());
				}
				finally
				{
					connection.Close();
				}
			}
		}

		protected int Executar(string sql, List<ParametroBD> parametros, bool EhProcedure = false)
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
				finally
				{
					conexao.Close();
				}
			}

		}

		private void AdicionarParametrosAQuery(SqlCommand sqlCommand, List<ParametroBD> parametros)
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