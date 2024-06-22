using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos.src.components.GPDropDownList
{
	public partial class GPDropDownList : System.Web.UI.UserControl
	{
		public string Style { get; set; }
		private string _cssClass;
		public string CssClass
		{
			get
			{
				return _cssClass;
			}
			set
			{
				_cssClass += value.Insert(0, " ");
			}
		}

		public Label LabelControl
		{
			get
			{
				return this.Label;
			}
		}
		public DropDownList DropDownListControl
		{
			get
			{
				return this.DropDownList;
			}
			set
			{
				this.DropDownList = value;
			}
		}
		public CustomValidator CustomValidatorControl
		{
			get
			{
				return this.CustomValidator;
			}
			set
			{
				this.CustomValidator = value;
			}
		}

		public string LabelText
		{
			get
			{
				return this.Label.Text;
			}
			set
			{
				this.Label.Text = value;
			}
		}
		public string ValidationGroup
		{
			get
			{
				return this.CustomValidator.ValidationGroup;
			}
			set
			{
				this.CustomValidator.ValidationGroup = value;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return this.CustomValidator.ErrorMessage;
			}
			set
			{
				this.CustomValidator.ErrorMessage = value;
			}
		}

		public object DataSource
		{
			get
			{
				return this.DropDownList.DataSource;
			}
			set
			{
				this.DropDownList.DataSource = value;
			}
		}

		public string DataTextField
		{
			get
			{
				return this.DropDownList.DataTextField;
			}
			set
			{
				this.DropDownList.DataTextField = value;
			}
		}

		public string DataValueField
		{
			get
			{
				return this.DropDownList.DataValueField;
			}
			set
			{
				this.DropDownList.DataValueField = value;
			}
		}

		public string SelectedValue
		{
			get
			{
				return this.DropDownList.SelectedValue;
			}
			set
			{
				this.DropDownList.SelectedValue = value;
			}
		}

		public override void DataBind()
		{
			this.DropDownList.DataBind();
		}

		public event ServerValidateEventHandler ServerValidate;
		protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			ServerValidate?.Invoke(source, args);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}