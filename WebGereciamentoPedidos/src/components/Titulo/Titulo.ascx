<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Titulo.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.Titulo.Titulo" %>
<style>
	.title-container {
		margin: 40px 0;
	}
</style>

<div class="title-container">
	<h1 runat="server">
		<asp:Label ID="TituloLabel" runat="server" Text="Título" ></asp:Label>
	</h1>
</div>
