using DAOGerenciamentoPedidos.Src;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.components.TituloMedio;
using WebGereciamentoPedidos.src.util;
using static WebGereciamentoPedidos.src.util.MensagemInfo;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages.FormAddEditProduto
{
	public partial class FormAddEditProduto : System.Web.UI.UserControl
	{
		public ProdutoDAO ProdutoDAO;
		public ModosFomularios ModoAtual
		{
			get
			{
				if (ViewState["ModoAtual"] != null)
				{
					return (ModosFomularios)ViewState["ModoAtual"];
				}
				return ModosFomularios.Cadastrar;
			}
			set
			{
				ViewState["ModoAtual"] = value;
			}
		}
		public Produto ProdutoASerEditado
		{
			get
			{
				if (ViewState["ProdutoASerEditado"] != null)
				{
					return (Produto)ViewState["ProdutoASerEditado"];
				}
				return null;
			}
			set
			{
				ViewState["ProdutoASerEditado"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ProdutoDAO = new ProdutoDAO();
		}

		public void AbrirForm(ModosFomularios modo, int? idProdutoParaEdicao)
		{
			FormAddEditProdutoPanel.Visible = true;
			ModoAtual = modo;
			if (modo == ModosFomularios.Cadastrar)
				ConfigurarFormParaCadastro();
			else if (idProdutoParaEdicao.HasValue && modo == ModosFomularios.Editar)
				ConfigurarFormParaEdicao(idProdutoParaEdicao.Value);
			
		}

		private void ConfigurarFormParaCadastro()
		{
			FormAddEditProdutoTituloMedio.Text = "Cadastrar Produto";
		}
		private void ConfigurarFormParaEdicao(int idProdutoParaEdicao)
		{
			try
			{
				ProdutoASerEditado = ProdutoDAO.ObterPorId(idProdutoParaEdicao);
				DescricaoTextFormField.Text = ProdutoASerEditado.Descricao;
				VlrUnitarioTextFormField.Text = ProdutoASerEditado.VlrUnitario.ToString();
				FormAddEditProdutoTituloMedio.Text = "Editar Produto";
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao obter produto para edição", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao obter produto para edição: {ex.ToString()}");
			}
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
				PageUtils.MostrarMensagemViaToast("Houve um erro ao validar campo descrição", TiposMensagem.Erro, Page);
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

		protected void CancelarButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(Request.RawUrl, true);
		}

		protected void SalvarButton_Click(object sender, EventArgs e)
		{
			if (!Page.IsValid)
				return;

			string descricaoProduto = DescricaoTextFormField.Text;
			if (!decimal.TryParse(VlrUnitarioTextFormField.Text, out decimal vlrUnitarioProduto))
			{
				PageUtils.MostrarMensagemViaToast("Favor informar valores numéricos no campo \"Valor Unitário\"", TiposMensagem.Erro, Page);
				return;
			}

			Produto produto = new Produto(null, descricaoProduto, vlrUnitarioProduto);

			if (ModoAtual == ModosFomularios.Cadastrar)
			{
				ProdutoDAO.Inserir(produto);
				//Antes de recarrregar a página, guarda a mensagem de sucesso numa session
				//para que a página principal possa exibi-la depois que for carregada
				Session["MensagemInfo"] = new MensagemInfo { Mensagem = "Produto cadastrado com sucesso", Tipo = TiposMensagem.Sucesso };
				Response.Redirect(Request.RawUrl, false);
			}
		}
	}
}