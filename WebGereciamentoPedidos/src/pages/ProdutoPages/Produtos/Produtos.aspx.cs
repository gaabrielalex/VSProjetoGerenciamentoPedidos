﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WebGereciamentoPedidos.src.dao;
using WebGereciamentoPedidos.src.models;
using WebGereciamentoPedidos.src.pages.ProdutoPages;
using WebGereciamentoPedidos.src.util;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages
{
	public partial class Produtos : System.Web.UI.Page
	{
		public ProdutoDAO ProdutoDAO;
		public List<Produto> DadosProdutosAtual
		{
			get
			{
				if (ViewState["DadosProdutosAtual"] != null)
				{
					return (List<Produto>)ViewState["DadosProdutosAtual"];
				}
				return new List<Produto>();
			}
			set
			{
				ViewState["DadosProdutosAtual"] = value;
			}
		}
		public readonly string VALOR_PADRAO_TITULO_PANEL_CADASTRO = "Cadastrar Produto";
		public readonly string VALOR_PADRAO_TEXTO_BOTAO_CADASTRAR = "Cadastrar";
		public readonly string VALOR_PADRAO_TITULO_PANEL_EDICAO = "Editar Produto";
		public readonly string VALOR_PADRAO_TEXTO_BOTAO_EDITAR = "Editar";
		public readonly string VALOR_PADRAO_TEXTO_BOTAO_CANCELAR = "Cancelar";
		public readonly bool VALOR_PADRAO_VISIBILIDADE_BOTAO_CANCELAR = false;

		protected void Page_Load(object sender, EventArgs e)
		{
			//Inicializando atributos
			ProdutoDAO = new ProdutoDAO();

			//Quando eu coloco esse ação aqui para chmar minha função que mostra o toast ele aparece no console do browser que a função não foi encontrada(not defined)
			//Page.ClientScript.RegisterStartupScript(typeof(Page), "showToast", "showToast('Erro ao deletar produto.', 'error');", true);
			//Não funciona tb, nem isso
			//Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "showModalMessage", "alert('teste');", true);

			if (!IsPostBack)
			{
				TratarCarregamentoDeDados();
				CarregarEstadoDeVariaveisEComponentesNecesarios();
			}
		}
		private void TratarCarregamentoDeDados()
		{
			if (Request.QueryString["Filtro"] != null)
			{
				string filtro = Request.QueryString["Filtro"];

				DadosProdutosAtual = ProdutoDAO.listarPorDescricao(filtro);
			}
			else
			{
				DadosProdutosAtual = ProdutoDAO.listar();
			}
			BindData();
		}
		private void BindData()
		{
			ProdutosGW.DataSource = DadosProdutosAtual;
			ProdutosGW.DataBind();
		}

		protected void ProdutosGW_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			//Teste: da erro
			//Page.ClientScript.RegisterStartupScript(typeof(Page), "showToast", "showToast('Erro ao deletar produto.', 'error');", true);
			int idProduto = Convert.ToInt32(e.CommandArgument);
			if (idProduto < 0)
			{
				PageUtils.mostrarMensagem("Houve um erro ao selecionar o produto para ação selecionada, entre em contato com suporte caso o erro persista.", "E", this);
			}
			else
			{
				Produto produtoSelecionado = new Produto();
				foreach (Produto produto in DadosProdutosAtual)
				{
					if (produto.IdProduto == idProduto)
					{
						produtoSelecionado = produto;
						break;
					}
				}

				if(e.CommandName == "Excluir")
				{
					bool confirmacaoExclusao = PageUtils.solicitarConfirmacao($"Deseja realmente excluir o produto \"{produtoSelecionado.Descricao}\"?");

					if (confirmacaoExclusao)
					{
						try
						{
							ProdutoDAO.excluir(produtoSelecionado.IdProduto ?? 0);
							TratarCarregamentoDeDados();
						}
						catch (Exception ex)
						{
							PageUtils.mostrarMensagem($"Erro ao deletar produto: {ex.Message}", "E", this);

						}
						finally
						{
							//Se cair no catch ele não vai executar esse função que esta tb sendo acionado no final
							//dessa função, desta forme o mesmo deve ser chamado aqui tb
							ImpedirResubimissaoDeFormulario(Response, Request, Context);
						}
					}
					
				}

				else if(e.CommandName == "Editar") {
					TituloPanelLabel.Text = VALOR_PADRAO_TITULO_PANEL_EDICAO;
					CadastrarProdutoButton.Text = VALOR_PADRAO_TEXTO_BOTAO_EDITAR;
					DescricaoProdutoTxtBox.Text = produtoSelecionado.Descricao;
					VlrUnitarioProdutoTxtBox.Text = produtoSelecionado.VlrUnitario.ToString();
					CancelarEdicaoButton.Visible = true;
					CamposProdutoPanel.Visible = true;

				}
			}

			ImpedirResubimissaoDeFormulario(Response, Request, Context);
		}

		protected void FiltrarButton_Click(object sender, EventArgs e)
		{
			String filtro = FiltrarTextBox.Text;
			//Obtem apenas a url sem possíveis query parameters no meio, desta
			//forma, ousuário não faz ter o mesmo query parametres na url
			String urlAtual = Request.Url.GetLeftPart(UriPartial.Path);
			String novaUrl;
			if (filtro == "" || filtro == null)
			{
				novaUrl = urlAtual;
			}
			else
			{
				novaUrl = urlAtual + $"?filtro={filtro}";
			}

			Response.Redirect(novaUrl, false);
		}
		protected void CadastrarProdutoLinkButton_Click(object sender, EventArgs e)
		{
			CamposProdutoPanel.Visible = !CamposProdutoPanel.Visible;
		}
		private void ArmazenarEstadoDeVariaveisEComponentesNecesarios()
		{
			Session["CamposProdutoPanelVisible"] = CamposProdutoPanel.Visible;
			Session["TituloPanelLabel.Text"] = TituloPanelLabel.Text;
			Session["CadastrarProdutoButton.Text"] = CadastrarProdutoButton.Text;
			Session["CancelarEdicaoButton.Visible"] = CancelarEdicaoButton.Visible;
			Session["DescricaoProdutoTxtBoxText"] = DescricaoProdutoTxtBox.Text;
			Session["VlrUnitarioProdutoTxtBoxText"] = VlrUnitarioProdutoTxtBox.Text;
		}
		private void CarregarEstadoDeVariaveisEComponentesNecesarios()
		{
			// Verificar e carregar o estado do painel
			if (Session["CamposProdutoPanelVisible"] != null)
			{
				CamposProdutoPanel.Visible = (bool)Session["CamposProdutoPanelVisible"];
			}
			else
			{
				CamposProdutoPanel.Visible = true; // Valor padrão
			}

			// Verificar e carregar o texto do título do painel
			if (Session["TituloPanelLabel.Text"] != null)
			{
				TituloPanelLabel.Text = Session["TituloPanelLabel.Text"].ToString();
			}
			else
			{
				TituloPanelLabel.Text = VALOR_PADRAO_TITULO_PANEL_CADASTRO; // Valor padrão
			}

			// Verificar e carregar o texto do botão de cadastro de produto
			if (Session["CadastrarProdutoButton.Text"] != null)
			{
				CadastrarProdutoButton.Text = Session["CadastrarProdutoButton.Text"].ToString();
			}
			else
			{
				CadastrarProdutoButton.Text = VALOR_PADRAO_TEXTO_BOTAO_CADASTRAR; // Valor padrão
			}

			// Verificar e carregar a visibilidade do botão de cancelar edição
			if (Session["CancelarEdicaoButton.Visible"] != null)
			{
				CancelarEdicaoButton.Visible = (bool)Session["CancelarEdicaoButton.Visible"];
			}
			else
			{
				CancelarEdicaoButton.Visible = false; // Valor padrão
			}

			// Verificar e carregar o texto da descrição do produto
			if (Session["DescricaoProdutoTxtBoxText"] != null)
			{
				DescricaoProdutoTxtBox.Text = Session["DescricaoProdutoTxtBoxText"].ToString();
			}
			else
			{
				DescricaoProdutoTxtBox.Text = ""; // Valor padrão
			}

			// Verificar e carregar o texto do valor unitário do produto
			if (Session["VlrUnitarioProdutoTxtBoxText"] != null)
			{
				VlrUnitarioProdutoTxtBox.Text = Session["VlrUnitarioProdutoTxtBoxText"].ToString();
			}
			else
			{
				VlrUnitarioProdutoTxtBox.Text = ""; // Valor padrão
			}
		}

		private void ImpedirResubimissaoDeFormulario(HttpResponse response, HttpRequest request, HttpContext context)
		{
			//A técnica usada exige que gerenciamos o estado de componentes
			//e variáveis necessáris através de recurso "Session"
			//O usuário é redirecionado para que seja impedido a resubimissao do formulario
			PageUtils.RedirecionarClienteParaEvitarResubimissaoDeFormulario(Response, Request, Context);
			//Porém, nesse momento o estado da página é perdido, o que não era a intenção
			//Desta forma, realizamos o gerenciamento de estado "manualmente"
			ArmazenarEstadoDeVariaveisEComponentesNecesarios();
			//No paload, se não for postback, carregamos novamente o estado anteior da página
			//recuperrabdo as informações via seesion
		}

		protected void DescricaoProdutCV_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string descricao = args.Value;

			//Validação se campo obrigatório
			if(descricao == "") {
				DescricaoProdutCV.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
			}

			//Validação de tamanho limite da string
			if(descricao.Length > 200)
			{
				DescricaoProdutCV.ErrorMessage = "Tamanho máximo de 200 caracteres excedido!";
				args.IsValid = false;
			}

			//Validação da já existência do produto
			bool produtoJaExiste = false;
			try {
				produtoJaExiste = ProdutoDAO.DescricaoJaExiste(descricao);
			} catch (Exception ex) {
				PageUtils.mostrarMensagem($"{ex.Message}", "E", this);
			}
			if (produtoJaExiste) {
				DescricaoProdutCV.ErrorMessage = "Produto já existente!";
				args.IsValid = false ;
			}
		}

		protected void VlrUnitarioProdutoCV_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string vlrUnitario = args.Value;

			//Continua para mim copilot
			//Validação se campo obrigatório
			if(vlrUnitario == "") {
				VlrUnitarioProdutoCV.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
				return;
			}

			//Validação de valor numérico
			if (!decimal.TryParse(vlrUnitario, out decimal vlrUnitarioDecimal)) {
				VlrUnitarioProdutoCV.ErrorMessage = "Valor inválido!";
				args.IsValid = false;
			}

			//Validação de valor máximo de dígitos
			string digitosString = vlrUnitarioDecimal.ToString();
			if(!digitosString.Contains(',')) {
				digitosString += ",0";
			}
			string[] digitos = (digitosString).Split(',');
			if (digitos[0].Length > 6 || digitos[1].Length > 2) {
				VlrUnitarioProdutoCV.ErrorMessage = "Valor deve ter no máximo 2 casas decimais e 6 dígitos!";
				args.IsValid = false;
			}
		}

		protected void CadastrarProdutoButton_Click(object sender, EventArgs e)
		{
			if (!Page.IsValid) {
				return;
			}
			string descricao = DescricaoProdutoTxtBox.Text;
			if(decimal.TryParse(VlrUnitarioProdutoTxtBox.Text, out decimal vlrUnitario)) {
				Produto produto = new Produto(null, descricao, vlrUnitario);
				try {
					ProdutoDAO.inserir(produto);
					PageUtils.mostrarMensagem("Produto cadastrado com sucesso!", "S", this);
					LimparCamposProduto();
					TratarCarregamentoDeDados();
				} catch (Exception ex) {
					
					PageUtils.mostrarMensagem($"Erro ao cadastrar produto: {ex.Message}", "E", this);
				} finally {
					ImpedirResubimissaoDeFormulario(Response, Request, Context);
				}
			} else {
				PageUtils.mostrarMensagem("Erro ao cadastrar produto: Valor unitário inválido!", "E", this);
			}
			ImpedirResubimissaoDeFormulario(Response, Request, Context);

		}

		private void LimparCamposProduto() {
			DescricaoProdutoTxtBox.Text = "";
			VlrUnitarioProdutoTxtBox.Text = "";
		}

		protected void CancelarEdicaoButton_Click(object sender, EventArgs e)
		{
			TituloPanelLabel.Text = VALOR_PADRAO_TITULO_PANEL_CADASTRO;
			CadastrarProdutoButton.Text = VALOR_PADRAO_TEXTO_BOTAO_CADASTRAR;
			DescricaoProdutoTxtBox.Text = "";
			VlrUnitarioProdutoTxtBox.Text = "";
			CancelarEdicaoButton.Visible = false;
		}
	}

}