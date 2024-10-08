﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextFormField.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.TextFormField" %>

<link rel="stylesheet" href="\src\components\TextFormField\TextFormField.css" />

<div class="gp-container-input <%=CssClass%>" style="<%=Style%>">
    <asp:Label ID="Label" runat="server"></asp:Label>
    <div class="gp-input">
        <input type="hidden" id="InputHiddenIdDado" runat="server" value=""/>
	    <asp:TextBox ID="TextBox" runat="server" CssClass="form-control" ></asp:TextBox>
        <div class="gp-input-container-custom-validator ">
            <asp:CustomValidator ID="CustomValidator" runat="server" ControlToValidate="TextBox" ErrorMessage=""
            CssClass="erro" OnServerValidate="CustomValidator_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
            <%--ValidateEmptyText="true" serve para que o validation seja chamado mesmo que o campo seja nulo, desta pode ser feito 
            também validação de campo obrigatório no lado do servidor sem usar o require requireValidaion--%>
        </div>
    </div>
</div>
