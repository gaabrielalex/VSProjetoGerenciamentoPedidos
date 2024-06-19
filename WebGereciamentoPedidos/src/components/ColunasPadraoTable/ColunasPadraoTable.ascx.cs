using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos.src.components.ColunasPadraoTable
{
	public partial class ColunasPadraoTable : System.Web.UI.UserControl
	{
		public string IdRegistro
		{
			get
			{
				if (ViewState["IdRegistro"] != null)
				{
					return ViewState["IdRegistro"].ToString();
				}
				return string.Empty;
			}
			set
			{
				ViewState["IdRegistro"] = value;
			}
		}

		public string MensagemConfirmacaoExclusao
		{
			get
			{
				if (ViewState["MensagemConfirmacaoExclusao"] != null)
				{
					return ViewState["MensagemConfirmacaoExclusao"].ToString();
				}
				return string.Empty;
			}
			set
			{
				ViewState["MensagemConfirmacaoExclusao"] = value;
			}	
		}
		public string UrlMetodoExclusao
		{
			get
			{
				if (ViewState["UrlMetodoExclusao"] != null)
				{
					return ViewState["UrlMetodoExclusao"].ToString();
				}
				return string.Empty;
			}
			set
			{
				ViewState["UrlMetodoExclusao"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}