using DAOGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.util;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages.FormAddEditProduto
{
	public partial class FormAddEditProduto : System.Web.UI.UserControl
	{
		public ProdutoDAO ProdutoDAO;
		public ModosFomularios ModoAtual
		{
			get
			{ 
				if(ViewState["ModoAtual"] != null) {
					return (ModosFomularios)ViewState["ModoAtual"];
				}
				return ModosFomularios.Cadastrar;
			}
			set 
			{
				ViewState["ModoAtual"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			ProdutoDAO = new ProdutoDAO();
		}

		public void abrirForm(ModosFomularios modo) 
		{
			FormAddEditProdutoPanel.Visible = true;
			ModoAtual = modo;
		}

		protected void DescricaoTextFormField_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string descricao = args.Value;

			//Validação se campo obrigatório
			if (descricao == "")
			{
				DescricaoTextFormField.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
			}

			//Validação de tamanho limite da string
			if (descricao.Length > 200)
			{
				DescricaoTextFormField.ErrorMessage = "Tamanho máximo de 200 caracteres excedido!";
				args.IsValid = false;
			}

			//Validação da já existência do produto
			bool produtoJaExiste = false;
			try
			{
				produtoJaExiste = ProdutoDAO.DescricaoJaExiste(descricao);
				if (produtoJaExiste)
				{
					DescricaoTextFormField.ErrorMessage = "Produto já existente!";
					args.IsValid = false;
				}
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao validar campo descrição", "e", Page);
				RegistroLog.Log($"Erro ao validar campo descrição: {ex.ToString()}");
			}
		}
		protected void VlrUnitarioTextFormField_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string vlrUnitario = args.Value;

			//Continua para mim copilot
			//Validação se campo obrigatório
			if (vlrUnitario == "")
			{
				VlrUnitarioTextFormField.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
				return;
			}

			//Validação de valor numérico
			if (!decimal.TryParse(vlrUnitario, out decimal vlrUnitarioDecimal))
			{
				VlrUnitarioTextFormField.ErrorMessage = "Valor inválido!";
				args.IsValid = false;
			}

			//Validação de valor máximo de dígitos
			string digitosString = vlrUnitarioDecimal.ToString();
			if (!digitosString.Contains(','))
			{
				digitosString += ",0";
			}
			string[] digitos = (digitosString).Split(',');
			if (digitos[0].Length > 6 || digitos[1].Length > 2)
			{
				VlrUnitarioTextFormField.ErrorMessage = "Valor deve ter no máximo 2 casas decimais e 6 dígitos!";
				args.IsValid = false;
			}
		}
	}
}