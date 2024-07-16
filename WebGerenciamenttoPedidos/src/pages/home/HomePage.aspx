<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebGerenciamenttoPedidos.src.pages.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
		main {
            margin: 0;
            padding: 0;
            font-family: arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 90vh;
        }

        .container {
            text-align: center;
        }

        .welcome-box {
            background-color: #fff;
            padding: 20px 40px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: #333;
            font-size: 24px;
        }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	 <div class="container">
        <div class="welcome-box">
            <h1>Bem-vindo ao sistema de gerenciamento de pedidos!</h1>
        </div>
    </div>
</asp:Content>
