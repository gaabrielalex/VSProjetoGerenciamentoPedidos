<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.PedidoPages.Pedidos.Pedidos" %>

<%@ Register TagPrefix="gp" TagName="ListagemPedidos" Src="~/src/pages/PedidoPages/ListagemPedidos/ListagemPedidos.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link rel="stylesheet" href="Pedido.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<gp:ListagemPedidos ID="ListagemPedidos" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript" src="Pedido.js"> </script>
</asp:Content>
