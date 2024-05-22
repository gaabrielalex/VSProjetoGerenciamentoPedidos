<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebGereciamentoPedidos._Default" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">
    <style>
	    .body-content {
            font-family: arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 90vh;
        }

        .container-default {
            text-align: center;
            margin-bottom: 50px;
            background-color blue;
        }

        .welcome-box {
            background-color: #fff;
            padding: 20px 40px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.9);
            margin-bottom: 70px;
        }

        h1 {
            color: #333;
            font-size: 24px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-default">
        <div class="welcome-box">
            <h1>Bem-vindo ao sistema de gerenciamento de pedidos!</h1>
        </div>
    </div>  

</asp:Content>
