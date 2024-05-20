using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebGerenciamenttoPedidos.src.utils
{
	public class DB_Connection
	{
		public static SqlConnection getConnection() {
			//Assim tava dadno erro
			//string connectionString = ConfigurationManager.ConnectionStrings["WebGerenciamenttoPedidos.Properties.Settings.db_connection"].ConnectionString;
			//return new SqlConnection(connectionString);

			//Corrigido, peguei a string direto nas propriedades do projeto
			return new SqlConnection("Data Source=GABRIELSILVA\\SQLEXPRESS;Initial Catalog=DB_Gerenciamento_Pedidos;Integrated Security=True;Encrypt=False;TrustServerCertificate=True");
		}

	}
}