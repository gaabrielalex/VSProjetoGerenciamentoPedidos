<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAddEditProduto.ascx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.FormAddEditProduto.FormAddEditProduto" %>
<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField/TextFormField.ascx" %>
<%@ Register TagPrefix="gp" TagName="Titulo" Src="~/src/components/Titulo/Titulo.ascx" %>

<style>
	.conteudo-FormAddEditProdutoPanel .row {
		margin-bottom: 20px;
	}

		.conteudo-FormAddEditProdutoPanel .row:last-child {
			margin-top: 30px;
		}
	.buttons-FormAddEditProdutoPanel {
		display: flex;
		justify-content: end;
		gap: 15px;
	}

	.conteudo-FormAddEditProdutoPanel {
		width: 400px;
	}

</style>

<asp:UpdatePanel runat="server">
	<ContentTemplate>
		<asp:Panel runat="server" ID="FormAddEditProdutoPanel" Visible="false">
			<gp:Titulo runat="server" Text="Cadastrar Produto" ID="FormAddEditProdutoTitulo"></gp:Titulo>
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
							CssClass="btn btn-lg btn-secondary SubmitButtonModalFormAddEditProduto" CausesValidation="false" />
						<asp:Button runat="server" ID="SalvarButton" ValidationGroup="CamposProduto" Text="Salvar"
							CssClass="btn btn-lg btn-primary SubmitButtonModalFormAddEditProduto" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>

