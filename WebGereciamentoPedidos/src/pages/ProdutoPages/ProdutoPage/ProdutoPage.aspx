<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProdutoPage.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.ProdutoPage.ProdutoPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<%@ Register TagPrefix="gp" TagName="FormAddEditProduto" Src="~/src/pages/ProdutoPages/FormAddEditProduto/FormAddEditProduto.ascx" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<gp:FormAddEditProduto runat="server" ID="FormAddEditProduto" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
