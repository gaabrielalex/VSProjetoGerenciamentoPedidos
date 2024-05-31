using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos.src.components
{
	public partial class Filtro : System.Web.UI.UserControl
	{
		public string Placeholder
		{ 
			get { return FiltroTextBox.Attributes["placeholder"]; }
			set { FiltroTextBox.Attributes["placeholder"] = value; }
		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}