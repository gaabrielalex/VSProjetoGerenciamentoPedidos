<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link href="ProdutosStyles.css" rel="stylesheet" />
    <style>

        .container-table {
			width: 100%;
            height: 70vh;
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

        .filtrar-container {
			margin-top: 40px;
		}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
            <asp:DataGrid 
                id="dgProdutos" 
                runat="server" 
                Width="100%"
                AutoGenerateColumns="False"
                AllowPaging="false"
                OnItemCommand="dgProdutos_ItemCommand"
               >
            <HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
                <Columns>
                    <asp:BoundColumn DataField="Descricao" HeaderText="Descrição" ></asp:BoundColumn>
                    <asp:BoundColumn DataField="VlrUnitario" HeaderText="Valor Unitário" ></asp:BoundColumn>
                    <asp:ButtonColumn Text="Editar" CommandName="Update" ButtonType="LinkButton">
                        <ItemStyle Width="100px" />
                    </asp:ButtonColumn>
                    <asp:ButtonColumn Text="Excluir" CommandName="Delete" ButtonType="LinkButton">
                        <ItemStyle Width="100px" />
                    </asp:ButtonColumn>
                </Columns>         
            </asp:DataGrid>
        </table>
    </div>
</asp:Content>
