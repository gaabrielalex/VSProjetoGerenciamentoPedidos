using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOGerenciamentoPedidos.Src.Data_Base
{
	public class DB_Connection
	{
		public static SqlConnection getConnection()
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
	}
}