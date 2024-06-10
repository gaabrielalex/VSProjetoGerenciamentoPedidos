<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAddEditProduto.ascx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.FormAddEditProduto.FormAddEditProduto" %>
<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField/TextFormField.ascx" %>
<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>

<link rel="stylesheet" href="/src/pages/ProdutoPages/FormAddEditProduto/FormAddEditProduto.css" />

<asp:UpdatePanel runat="server">
	<ContentTemplate>
		<asp:Panel runat="server" ID="FormAddEditProdutoPanel" Visible="false" DefaultButton="SalvarButton">
			<gp:TituloMedio runat="server" ID="FormAddEditProdutoTituloMedio"></gp:TituloMedio>
			<div class="conteudo-FormAddEditProdutoPanel">
				<div class="row">
					<gp:TextFormField runat="server" ID="DescricaoTextFormField" LabelText="Descrição" Style="margin-right: 20px"
						ValidationGroup="CamposProduto" OnServerValidate="DescricaoTextFormField_ServerValidate" />
				</div>
				<div class="row d-flex justify-content-end"">
					<gp:TextFormField runat="server" ID="VlrUnitarioTextFormField" LabelText="Valor Unitário" Format="dinheiro"
						ValidationGroup="CamposProduto" OnServerValidate="VlrUnitarioTextFormField_ServerValidate" />
				</div>
				<div class="row">
					<div class="buttons-FormAddEditProdutoPanel">
						<asp:Button runat="server" ID="CancelarButton" ValidationGroup="CamposProduto" Text="Cancelar" Onclick="CancelarButton_Click"
							CssClass="btn btn-secondary SubmitButtonModalFormAddEditProduto" CausesValidation="false" />
						<asp:Button runat="server" ID="SalvarButton" ValidationGroup="CamposProduto" Text="Salvar" OnClick="SalvarButton_Click"
							CssClass="btn btn-primary SubmitButtonModalFormAddEditProduto" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>