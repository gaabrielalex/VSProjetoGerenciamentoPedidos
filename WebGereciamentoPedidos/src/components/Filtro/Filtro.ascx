<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Filtro.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.Filtro" %>


<link rel="stylesheet" href="\src\components\Filtro\Filtro.css" />


<div>
	<asp:TextBox cssClass="FiltroTextBox" runat="server" ID="FiltroTextBox"> </asp:TextBox>
	<asp:Button class="FiltroButton" runat="server" ID="FiltroButton" Text="Filtrar" OnClick="FiltroButton_Click" />
</div>