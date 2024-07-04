<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAddEditItemPedido.ascx.cs" Inherits="WebGereciamentoPedidos.src.pages.ItemPedidoPages.FormAddEditItemPedido.FormAddEditItemPedido" %>

<%@ Register TagPrefix="gp" TagName="PesquisaLookUpProdutos" Src="~/src/pages/ProdutoPages/PesquisaLookUpProdutos/PesquisaLookUpProdutos.ascx" %>
<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField/TextFormField.ascx" %>
<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>

<link rel="stylesheet" href="/src/pages/ItemPedidoPages/FormAddEditItemPedido/FormAddEditItemPedido.css" />

<script type="text/javascript" src="/src/pages/ItemPedidoPages/FormAddEditItemPedido/FormAddEditItemPedido.js"> </script>

<gp:PesquisaLookUpProdutos runat="server" ID="PesquisaLookUpProdutos" ScriptAoSelecionarProduto="myApp.FormAddEditItemPedido.carregarInformacoesProduto" />

<asp:UpdatePanel runat="server">
	<ContentTemplate>
		<asp:Panel runat="server" ID="FormAddEditItemPedidoPanel" DefaultButton="SalvarItemPedidoButton">
			<gp:TituloMedio runat="server" Text="Cadastrar Item
			" ID="FormAddEditItemPedidoTituloMedio"></gp:TituloMedio>
			<div class="conteudo-FormAddEditItemPedidoPanel">
				<div class="row row-cols-auto">
					<div class="flex-grow-1">
						<gp:TextFormField runat="server" ID="ProdutoTextFormField" LabelText="Produto" Enabled="false"
							ValidationGroup="CamposItemPedido" CssClass="CampoDescricaoProduto" OnServerValidate="ProdutoTextFormField_ServerValidate" />
					</div>
					<div class="d-flex" style="display: flex; align-items: center; padding-top: 4px">
						<asp:Button runat="server" ID="PesquisarProdutoButton" Text="Pesquisar" OnClientClick="myApp.PesquisaLookUpProdutos.abrirModalPesquisa(); return false;"
							Style="height: 38px" CssClass="btn btn-primary SubmitButtonModalFormAddEditItemPedido" CausesValidation="false" />
					</div>
				</div>
				<div class="row">
					<gp:TextFormField runat="server" ID="QtdeTextFormField" LabelText="Quantidade" TextMode="Number"
						ValidationGroup="CamposItemPedido" OnServerValidate="QtdeTextFormField_ServerValidate" />
				</div>

				<div class="row">
					<div class="buttons-FormAddEditItemPedidoPanel">
						<asp:Button runat="server" ID="CancelarItemPedidoButton" ValidationGroup="CamposItemPedido" Text="Cancelar" OnClick="CancelarItemPedidoButton_Click"
							CssClass="btn btn-secondary SubmitButtonModalFormAddEditItemPedido" CausesValidation="false" />
						<asp:Button runat="server" ID="SalvarItemPedidoButton" ValidationGroup="CamposItemPedido" Text="Salvar" OnClick="SalvarItemPedidoButtonn_Click"
							CssClass="btn btn-primary SubmitButtonModalFormAddEditItemPedido" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
	var myApp = myApp || {};
	myApp.FormAddEditItemPedido = {
		InputHiddenIdProduto: "<%=ProdutoTextFormField.InputHiddenIdDadox.ClientID %>",
	};
</script>