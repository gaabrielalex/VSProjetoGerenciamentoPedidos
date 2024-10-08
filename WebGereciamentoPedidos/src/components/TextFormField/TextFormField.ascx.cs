﻿using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebGereciamentoPedidos.src.components
{
	public partial class TextFormField : System.Web.UI.UserControl
	{
		public string IdDado
		{
			get
			{
				return InputHiddenIdDado.Value;
			}
			set
			{
				InputHiddenIdDado.Value = value;
			}
		}
		public HtmlInputHidden InputHiddenIdDadox
		{
			get
			{
				return InputHiddenIdDado;
			}
		}
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
		private string _format;
		public string Format
		{
			get
			{
				return _format;
			}
			set 
			{
				if (value == "dinheiro") {
					TextBoxControl.CssClass += "dinheiro".Insert(0, " ");
					_format = value;
				}
			}
		}

		public Label LabelControl
		{
			get
			{
				return this.Label;
			}
		}
		public TextBox TextBoxControl
		{
			get 
			{
				return this.TextBox;
			}
			set
			{
				this.TextBox = value;
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
		public string Text
		{
			get
			{
				return this.TextBox.Text;
			}
			set
			{
				this.TextBox.Text = value;
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

		public bool Enabled
		{
			get
			{
				return this.TextBox.Enabled;
			}
			set
			{
				this.TextBox.Enabled = value;
			}
		}

		public bool EhMultiLinha
		{
			get
			{
				return this.TextBox.TextMode == TextBoxMode.MultiLine;
			}
			set
			{
				if (value)
				{
					this.TextBox.TextMode = TextBoxMode.MultiLine;
				}
			}
		}

		public TextBoxMode TextMode
		{
			get
			{
				return this.TextBox.TextMode;
			}
			set
			{
				this.TextBox.TextMode = value;
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