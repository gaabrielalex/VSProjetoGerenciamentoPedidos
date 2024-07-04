using DAOGerenciamentoPedidos;
using DAOGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_;
using WebGereciamentoPedidos.src.util;
using static WebGereciamentoPedidos.src.util.MensagemInfo;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages.FormAddEditProduto
{
	public partial class FormAddEditProduto : System.Web.UI.UserControl
	{
		private readonly ProdutoDAO _produtoDAO = new ProdutoDAO(new BancoDeDados());

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
			if (!Page.IsPostBack)
			{
				if (Request.QueryString["id"] != null)
				{
					var idProdutoParaEdicao = int.Parse(Request.QueryString["id"]);
					ConfigurarForm(ModosFomularios.Editar, idProdutoParaEdicao);
				}
				else
				{
					ConfigurarForm(ModosFomularios.Cadastrar, null);
				}
			}
		}

		public void ConfigurarForm(ModosFomularios modo, int? idProdutoParaEdicao)
		{
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
				ProdutoASerEditado = _produtoDAO.ObterPorId(idProdutoParaEdicao);
				if (ProdutoASerEditado == null)
				{
					TratarProdutoNaoEncontrado();
					return;
				}
				FormAddEditProdutoTituloMedio.Text = "Editar Produto";
				DescricaoTextFormField.Text = ProdutoASerEditado.Descricao;
				VlrUnitarioTextFormField.Text = ProdutoASerEditado.VlrUnitario.ToString();
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
			if (descricao == string.Empty)
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
				if ((ProdutoASerEditado != null && !ProdutoASerEditado.Descricao.Equals(descricao) && ModoAtual == ModosFomularios.Editar) || ModoAtual == ModosFomularios.Cadastrar)
				{

					produtoJaExiste = _produtoDAO.DescricaoJaExiste(descricao);
					if (produtoJaExiste)
					{
						DescricaoTextFormField.ErrorMessage = "Produto já existente!";
						args.IsValid = false;
					}
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

			//Validação se campo obrigatório
			if (vlrUnitario == string.Empty)
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
				VlrUnitarioTextFormField.ErrorMessage = "Valor deve ter no máximo 8 dígitos, sendo 6 inteiros e 2 decimais!";
				args.IsValid = false;
			}
		}

		protected void CancelarButton_Click(object sender, EventArgs e)
		{
			PageUtils.RedirecionarParaPagina(Page, Request, ProdutosNovo.CaminhoPagina);
		}

		protected void SalvarButton_Click(object sender, EventArgs e)
		{
			if (!Page.IsValid)
			{
				PageUtils.FecharLoadingModal(Page);
				return;
			}

			Produto produto = ObterDadosDoFormulario();
			if (produto == null)
				return;

			if (ModoAtual == ModosFomularios.Cadastrar)
			{
				CadastarProduto(produto);
			}
			else if (ModoAtual == ModosFomularios.Editar)
			{
				EditarProduto(produto);
			}
		}

		private Produto ObterDadosDoFormulario()
		{
			string descricaoProduto = DescricaoTextFormField.Text;
			if (!decimal.TryParse(VlrUnitarioTextFormField.Text, out decimal vlrUnitarioProduto))
			{
				PageUtils.MostrarMensagemViaToast("Favor informar valores numéricos no campo \"Valor Unitário\"", TiposMensagem.Erro, Page);
				return null;
			}
			return new Produto(null, descricaoProduto, vlrUnitarioProduto);
		}

		private void CadastarProduto(Produto produto)
		{
			try
			{
				_produtoDAO.Inserir(produto);
				PageUtils.RedirecionarParaPagina(
					page: Page,
					request: Request,
					urlPagina: ProdutosNovo.CaminhoPagina,
					MensagemAposRedirecionamento: "Produto cadastrado com sucesso",
					tipoMensagem: TiposMensagem.Sucesso
				);
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao cadastrar o produto", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao cadatsrar produto: {ex.ToString()}");
			}
		}

		private void EditarProduto(Produto produto)
		{
			try
			{
				_produtoDAO.Editar(produto, ProdutoASerEditado.IdProduto.Value);
				PageUtils.RedirecionarParaPagina(
					page: Page,
					request: Request,
					urlPagina: ProdutosNovo.CaminhoPagina,
					MensagemAposRedirecionamento: "Produto editado com sucesso",
					tipoMensagem: TiposMensagem.Sucesso
				);
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao editar o produto", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao editar produto: {ex.ToString()}");
			}
		}

		private void TratarProdutoNaoEncontrado()
		{
			PageUtils.RedirecionarParaPagina(
				page: Page,
				request: Request,
				urlPagina: ProdutosNovo.CaminhoPagina,
				MensagemAposRedirecionamento: "Produto não encontrado!",
				tipoMensagem: TiposMensagem.Erro
			);
		}
	}
}