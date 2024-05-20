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
			string connectionString = ConfigurationManager.ConnectionStrings["WebGerenciamenttoPedidos.Properties.Settings.db_connectiono"].ConnectionString;
			return new SqlConnection(connectionString);
		}

	}
}