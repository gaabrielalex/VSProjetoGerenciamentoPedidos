<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPicker.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.DataPicker.DataPicker" %>


<div class="gp-container-input <%=CssClass%>" style="<%=Style%>">
    <asp:Label ID="Label" runat="server"></asp:Label>
    <div class="gp-input">
        <input ID="Datetime" runat="server" type="datetime-local" name="datetime" class="form-control" />
        <div class="gp-input-container-custom-validator ">
            <asp:CustomValidator ID="CustomValidator" runat="server" ControlToValidate="Datetime" ErrorMessage=""
            CssClass="erro" OnServerValidate="CustomValidator_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
        </div>
    </div>
</div>
