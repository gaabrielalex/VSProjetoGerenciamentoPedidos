<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColunasPadraoTable.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.ColunasPadraoTable.ColunasPadraoTable" %>

<div style="display:flex; justify-content:space-evenly">
	<asp:LinkButton ID="EditarLK" runat="server" CommandName="Editar"
		CommandArgument=<%# IdRegistro %> Text="Editar" CausesValidation="False">
	</asp:LinkButton>

	<asp:LinkButton ID="ExcluirLK" runat="server" CommandName="Excluir" CssClass="excluir-lk"
		CommandArgument=<%# IdRegistro %> Text="Excluir" CausesValidation="False" 
		data-id-registro=<%# IdRegistro %>
		data-mensagem-confirmacao-exclusao=<%# MensagemConfirmacaoExclusao %>
		data-url-metodo-exclusao=<%# UrlMetodoExclusao %>
		>
	</asp:LinkButton>
</div>

<script type="text/javascript">
	var myApp = {
		idExcluirLK: "<%=ExcluirLK.ClientID %>"
	}
</script>

<script type="text/javascript" src="\src\components\ColunasPadraoTable\ColunasPadraoTable.js"> </script>