<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PedidoPage.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.PedidoPages.PedidoPage.PedidoPage" %>

<%@ Register TagPrefix="gp" TagName="FormAddEditPedido" Src="~/src/pages/PedidoPages/FormAddEditPedido/FormAddEditPedido.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<gp:FormAddEditPedido runat="server" ID="FormAddEditPedido" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
