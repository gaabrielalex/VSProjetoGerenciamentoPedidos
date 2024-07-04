using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages.PesquisaLookUpProdutos
{
	public partial class PesquisaLookUpProdutos : System.Web.UI.UserControl
	{
		public string ScriptAoSelecionarProduto
		{
			get
			{
				return ListagemProdutos.ScriptAoSelecionarProduto;
			}
			set
			{
				ListagemProdutos.ScriptAoSelecionarProduto = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}