<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link href="ProdutosStyles.css" rel="stylesheet" />
    <style>

        .container-table {
			width: 100%;
            height: 60vh;
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

        .erro {
	        color: #dc3545;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Modal para edição de produtos -->
    <!-- Botão para abrir o modal -->
    <button type="button" class="btn btn-primary">
      Launch static backdrop modal
    </button>

    <!-- Modal -->
    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="staticBackdropLabel">Modal title</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            ...
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Understood</button>
          </div>
        </div>
      </div>
    </div>

    <!-- Parte de cadastro de produtos -->
	<div class="divCadastrarProdutoLinkButton">
		<asp:LinkButton CssClass="CadastrarProdutoLinkButton" ID="CadastrarProdutoLinkButton" runat="server" OnClick="CadastrarProdutoLinkButton_Click">
            <h1 runat="server">
                <asp:Label ID="TituloPanelLabel" runat="server" Text="Cadastrar Produto"></asp:Label>
            </h1>
		</asp:LinkButton>
	</div>
	<asp:Panel class="camposProdutoPanel" ID="CamposProdutoPanel" runat="server" Visible="true">
		<asp:Label ID="DescricaoProdutoLabel" runat="server" Text="Descrição:">
		</asp:Label>
        <div class="div-campo container-campo-DescricaoProdutoTxtBox">
			<asp:TextBox ID="DescricaoProdutoTxtBox" runat="server"></asp:TextBox>
            <%--ValidateEmptyText="true" serve para que o validation seja chamado mesmo que o 
            campo seja nulo, desta pode ser feito também validação de campo obrigatório--%>
            <asp:CustomValidator ID="DescricaoProdutCV" runat="server" ControlToValidate="DescricaoProdutoTxtBox"  ErrorMessage="" 
            CssClass="erro" ValidationGroup="CamposProduto" OnServerValidate="DescricaoProdutCV_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
        </div>

		<asp:Label class="teste" ID="VlrUnitarioProdutoLabel" runat="server" Text="Vlr. Unitário:">
		</asp:Label>
        <div class="div-campo">
			<asp:TextBox ID="VlrUnitarioProdutoTxtBox" runat="server" CssClass="dinheiro"></asp:TextBox>
            <asp:CustomValidator ID="VlrUnitarioProdutoCV" runat="server" ControlToValidate="VlrUnitarioProdutoTxtBox"  ErrorMessage="" 
            CssClass="erro " ValidationGroup="CamposProduto" OnServerValidate="VlrUnitarioProdutoCV_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
        </div>

		<asp:Button runat="server" ID="CadastrarProdutoButton" Text="Cadastrar" ValidationGroup="CamposProduto" OnClick="CadastrarProdutoButton_Click"/>
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
                    <asp:ButtonColumn Text="Editar" CommandName="Editar" ButtonType="LinkButton">
                        <ItemStyle Width="100px" />
                    </asp:ButtonColumn>
                    <asp:ButtonColumn Text="Excluir" CommandName="Excluir" ButtonType="LinkButton">
                        <ItemStyle Width="100px" />
                    </asp:ButtonColumn>
                </Columns>         
            </asp:DataGrid>
        </table>
    </div>
</asp:Content>
