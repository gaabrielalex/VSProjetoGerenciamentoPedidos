<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos" %>

<%@ Register TagPrefix="gp" TagName="TextFormField" Src="~/src/components/TextFormField.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link href="ProdutosStyles.css" rel="stylesheet" />
    <style>

        .container-table {
			width: 100%;
            height: 55vh;
			overflow-x: auto;
            margin-top: 35px;
		}
        .table {
            width: 100%;
            border-collapse: collapse;
            max-height: 100px;
        }

        .table th, .table td {
            padding: 10px;
            border: 1px solid #ddd;
        }

        .table th {
            background-color: #212529;
            color: white;
            font-weight: bold;
        }

        .table-striped tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .table-header {
            background-color: #212529;
            color: white;
            font-weight: bold;
        }

        .table-cell {
            padding: 10px;
            border: 1px solid #ddd;
        }

        .table-cell-header {
            background-color: #212529;
            color: white;
        }

        td {
            border-bottom: 1px solid #ccc; /* Adjust color and style as needed */
        }

        .divCadastrarProdutoLinkButton, .camposProdutoPanel, .separadorCamposRegistros, .filtrar-container, .container-table {
            margin-top: 35px;
        }

        .divCadastrarProdutoLinkButton h1 {
            color: black;
        }

        .divCadastrarProdutoLinkButton a {
            text-decoration: none;
        }

        .camposProdutoPanel  input[type="text"] {
            min-height: 30px;
            width: 280px;
        }
        .camposProdutoPanel {
            font-size: 18px
        }

        .div-campo {
            display: inline-flex;
            flex-direction: column;
            margin: 0px 15px;
            max-width: 280px;
        }

        .CancelarEdicaoButton {
            margin-left: 15px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Parte de cadastro de produtos -->
	<div class="divCadastrarProdutoLinkButton">
		<asp:LinkButton CssClass="CadastrarProdutoLinkButton" ID="CadastrarProdutoLinkButton" runat="server" OnClick="CadastrarProdutoLinkButton_Click">
            <h1 runat="server">
                <asp:Label ID="TituloPanelLabel" runat="server" Text="Cadastrar Produto"></asp:Label>
            </h1>
		</asp:LinkButton>
	</div>
	<asp:Panel class="camposProdutoPanel" ID="CamposProdutoPanel" runat="server" Visible="true">
        <gp:TextFormField runat="server" ID="DescricaoTextFormField" 
            LabelText="Descrição" ValidationGroup="CamposProduto" OnServerValidate="DescricaoTextFormField_ServerValidate"/>

		<asp:Label class="teste" ID="VlrUnitarioProdutoLabel" runat="server" Text="Vlr. Unitário:"></asp:Label>
        <div class="div-campo">
			<asp:TextBox ID="VlrUnitarioProdutoTxtBox" runat="server" CssClass="dinheiro"></asp:TextBox>
            <asp:CustomValidator ID="VlrUnitarioProdutoCV" runat="server" ControlToValidate="VlrUnitarioProdutoTxtBox"  ErrorMessage="" 
            CssClass="erro" ValidationGroup="CamposProduto" OnServerValidate="VlrUnitarioProdutoCV_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
        </div>

		<asp:Button runat="server" ID="CadastrarEditarProdutoButton" Text="Cadastrar" ValidationGroup="CamposProduto" OnClick="CadastrarEditarProdutoButton_Click" />
        <asp:Button runat="server" CssClass="CancelarEdicaoButton" ID="CancelarEdicaoButton" Text="Cancelar" OnClick="CancelarEdicaoButton_Click"
            ValidationGroup="NuloParaNaoMeImpedirDeCancelarAEdicaoSemQueTodosOsCamposEstejamValidos" Visible="false"/>
	</asp:Panel>

    <hr class="separadorCamposRegistros" />
    

    <!-- Parte de listagem de produtos -->
    <div class="filtrar-container">
        <asp:TextBox cssClass="FiltrarTextBox" class="FiltrarTextBox" runat="server" ID="FiltrarTextBox" placeholder="Filtre pela descrição..." > </asp:TextBox>
        <asp:Button class="FiltrarButton" runat="server" ID="FiltrarButton" Text="Filtrar" OnClick="FiltrarButton_Click" />
    </div>
    <div class="container-table">
        <table class="table table-striped " cellSpacing="1" cellPadding="1" width="100%" border="0">
            <tr>
	            <td>
		            <asp:Label ID="lblTitulo" runat="server" Text="Produtos"></asp:Label>
	            </td>
            </tr>
            <asp:GridView 
                ID="ProdutosGW" 
                runat="server" 
                AutoGenerateColumns="False"
                Width="100%"
                AllowPaging="false"
                OnRowCommand="ProdutosGW_RowCommand"
                >
            <HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
            <Columns>
                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                <asp:BoundField DataField="VlrUnitario" HeaderText="Valor Unitário" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="EditarLK" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdProduto") %>'
                            Text="Editar">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ExcluirLK" runat="server" CommandName="Excluir" CommandArgument='<%# Eval("IdProduto") %>'
                            Text="Excluir">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </table>
    </div>
</asp:Content>
