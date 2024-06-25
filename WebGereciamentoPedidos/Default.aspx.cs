using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos
{
	public partial class _Default : Page
	{
		public string TextoModoProducao
		{
			get
			{
				if (System.Configuration.ConfigurationManager.AppSettings["ModoProducao"] == null 
					|| System.Configuration.ConfigurationManager.AppSettings["ModoProducao"] == string.Empty
					|| System.Configuration.ConfigurationManager.AppSettings["ModoProducao"].ToLower() == "false"
				)
				{
					return "Bem-vindo ao sistema de gerenciamento de pedidos em modo de desenvolvimento!";
				} else {
					return "Bem-vindo ao sistema de gerenciamento de pedidos!";
				}
				return System.Configuration.ConfigurationManager.AppSettings["ModoProducao"];
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}