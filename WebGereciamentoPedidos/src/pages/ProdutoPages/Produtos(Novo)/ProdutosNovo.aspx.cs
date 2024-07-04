using DAOGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
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
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.util;
using static WebGereciamentoPedidos.src.util.MensagemInfo;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_
{
	public partial class ProdutosNovo : System.Web.UI.Page
	{
		public static readonly string CaminhoPagina = "~/src/pages/ProdutoPages/Produtos(Novo)/ProdutosNovo.aspx";
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static string ExcluirProduto(int id)
		{
			new ProdutoDAO(new BancoDeDados()).Excluir(id);

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