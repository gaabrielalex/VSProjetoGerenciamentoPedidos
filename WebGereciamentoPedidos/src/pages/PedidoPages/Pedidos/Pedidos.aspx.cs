using DAOGerenciamentoPedidos.Src;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static WebGereciamentoPedidos.src.util.MensagemInfo;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.pages.ProdutoPages.FormAddEditProduto;
using WebGereciamentoPedidos.src.util;
using DAOGerenciamentoPedidos;
using DAOGerenciamentoPedidos.Src.Data_Base;

namespace WebGereciamentoPedidos.src.pages.PedidoPages.Pedidos
{
	public partial class Pedidos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static string ExcluirPedido(int id)
		{
			new PedidoDAO(new BancoDeDados()).Excluir(id);

			var response = new
			{
				Message = $"Registro {id} excluído com sucesso",
				Success = true
			};

			// Serializa o objeto para JSON
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize(response);
		}

	}
}