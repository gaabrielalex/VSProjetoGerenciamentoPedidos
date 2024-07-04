<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProdutosNovo.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_.ProdutosNovo" %>

<%@ Register TagPrefix="gp" TagName="ListagemProdutos" Src="~/src/pages/ProdutoPages/ListagemProdutos/ListagemProdutos.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link rel="stylesheet" href="ProdutosNovo.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<gp:ListagemProdutos runat="server" ID="ListagemProdutos" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript" src="ProdutoNovo.js"> </script>
</asp:Content>
