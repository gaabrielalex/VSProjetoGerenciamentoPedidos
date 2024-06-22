<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAddEditPedido.ascx.cs" Inherits="WebGereciamentoPedidos.src.pages.PedidoPages.FormAddEditPedido.FormAddEditPedido" %>

<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField/TextFormField.ascx" %>
<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>
<%@ Register TagPrefix="gp" TagName="DropDownList" Src="~/src/components/GPDropDownList/GPDropDownList.ascx" %>
<%@ Register TagPrefix="gp" TagName="DataPicker" Src="~/src/components/DataPicker/DataPicker.ascx" %>

<link rel="stylesheet" href="/src/pages/PedidoPages/FormAddEditPedido/FormAddEditPedido.css" />

<asp:UpdatePanel runat="server">
	<ContentTemplate>
		<asp:Panel runat="server" ID="FormAddEditPedidoPanel" Visible="false" DefaultButton="SalvarButton">
			<gp:TituloMedio runat="server" ID="FormAddEditPedidoTituloMedio"></gp:TituloMedio>
			<div class="conteudo-FormAddEditPedidoPanel">
				<div class="row">
					<gp:TextFormField runat="server" ID="ClienteTextFormField" LabelText="Cliente"
						ValidationGroup="CamposPedido" OnServerValidate="ClienteTextFormField_ServerValidate" />
				</div>
				<div class="row">
					<gp:DropDownList runat="server" ID="MetodoPagtoDropDownList" LabelText="Método de Pagamento"
						ValidationGroup="CamposPedido" />
				</div>
				<div class="row">
					<gp:TextFormField runat="server" ID="VlrSubtotalTextFormField" LabelText="Valor Subtotal" Format="dinheiro"
						ValidationGroup="CamposPedido" Enabled="false" />
				</div>
				<div class="row">
					<gp:TextFormField runat="server" ID="DescontoTextFormField" LabelText="Desconto" Format="dinheiro"
						ValidationGroup="CamposPedido" OnServerValidate="DescontoTextFormField_ServerValidate" />
				</div>
				<div class="row">
					<gp:TextFormField runat="server" ID="VlrTotalTextFormField" LabelText="Valor Total"
						ValidationGroup="CamposPedido" Enabled="false" />
				</div>
				<div class="row">
					<gp:DataPicker runat="server" ID="DataHoraPedidoDataPicker" LabelText="Data/hora"
						ValidationGroup="CamposPedido" OnServerValidate="DataHoraPedidoDataPicker_ServerValidate" />
				</div>
				<div class="row">
					<gp:DropDownList runat="server" ID="StatusDropDownList" LabelText="Status"
						ValidationGroup="CamposPedido" />
				</div>
				<div class="row">
					<div class="buttons-FormAddEditPedidoPanel">
						<asp:Button runat="server" ID="CancelarButton" ValidationGroup="CamposPedido" Text="Cancelar" OnClick="CancelarButton_Click"
							CssClass="btn btn-secondary SubmitButtonModalFormAddEditPedido" CausesValidation="false" />
						<asp:Button runat="server" ID="SalvarButton" ValidationGroup="CamposPedido" Text="Salvar" OnClick="SalvarButton_Click"
							CssClass="btn btn-primary SubmitButtonModalFormAddEditPedido" />
					</div>
				</div>
			</div>

		</asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
	// Carregar o script dinamicamente e inicializar a aplicação
	document.addEventListener('DOMContentLoaded', function () {
		initializeScripts();

	});

	// Garantir que a função seja executada após atualizações do UpdatePanel
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
		initializeScripts();
	});

	function initializeScripts() {
		$.getScript('/src/pages/PedidoPages/FormAddEditPedido/FormAddEditPedido.js', () => { });

		myApp.idInputVlrSubtotal = "<%=VlrSubtotalTextFormField.TextBoxControl.ClientID %>";
		myApp.idInputDesconto = "<%=DescontoTextFormField.TextBoxControl.ClientID %>";
		myApp.idInputVlrTotal = "<%=VlrTotalTextFormField.TextBoxControl.ClientID %>";
		myApp.idSpanValidacaoDesconto = "<%=DescontoTextFormField.CustomValidatorControl.ClientID %>";
	}
</script>
