using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos.src.components.DataPicker
{
	public partial class DataPicker : System.Web.UI.UserControl
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
		//private string _format;
		//public string Format { get; set; }

		public Label LabelControl
		{
			get
			{
				return this.Label;
			}
		}
		public HtmlInputGenericControl TextBoxControl
		{
			get
			{
				return this.Datetime;
			}
			set
			{
				this.Datetime = value;
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
		public DateTime Date
		{
			get
			{
				return DateTime.TryParseExact
					(this.Datetime.Value, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) 
						? date 
						: DateTime.MinValue;
			}
			set
			{
				this.Datetime.Value = value.ToString("yyyy-MM-ddTHH:mm"); // Formato específico para datetime-local
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