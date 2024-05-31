<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Filtro.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.Filtro" %>

<style>
	.FiltroTextBox, .FiltroButton{
		min-height: 42px;
		margin-right: 0;
	}

	.FiltroTextBox {
		width: 400px;
		max-width: 400px;
	}
</style>

<div>
	<asp:TextBox cssClass="FiltroTextBox" runat="server" ID="FiltroTextBox"> </asp:TextBox>
	<asp:Button class="FiltroButton" runat="server" ID="FiltroButton" Text="Filtrar" />
</div>