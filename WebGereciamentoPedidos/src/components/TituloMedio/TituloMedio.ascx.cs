using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos.src.components.TituloMedio
{
	public partial class TituloMedio : System.Web.UI.UserControl
	{
		public string Text
		{
			get
			{
				return TituloLabel.Text;
			}
			set
			{
				TituloLabel.Text = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}