<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalConfirmacaoExclusao.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.ModalConfirmacaoExclusao.ModalConfirmacaoExclusao" %>

<div class="modal fade" id="modalConfirmacaoExclusao" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<h1 class="modal-title fs-5" id="modalConfirmacaoExclusaoLabel">Confirmação</h1>
				<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
			</div>
			<div class="modal-body">
				...
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
				<button type="button" class="btn btn-primary btn-sim">Sim</button>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function fecharModal() {
		$('#modalConfirmacaoExclusao').modal('hide');
	}

	function abriModalConfirmacaoExclusaoComAjax({ idRegistro, descricaoRegistro, urlMetodo, callbackSucesso, callbackErro, complete }) {
		const dados = {
			id: idRegistro,
		}
		const beforeSend = fecharModal;

		$('#modalConfirmacaoExclusao .modal-body').text(`Tem certeza que deseja excluir o registro "${descricaoRegistro}"?`);
		$('#modalConfirmacaoExclusao .btn-sim').off('click').on('click', () => confirmarExclusaoComAjax(
			{ urlMetodo, dados, beforeSend, callbackSucesso, callbackErro, complete }
		));
		$('#modalConfirmacaoExclusao').modal('show');
		return false; // Retorne false para evitar postback
	}

	function confirmarExclusaoComAjax({ urlMetodo, dados, beforeSend, callbackSucesso, callbackErro, complete }) {
		$.ajax({
			type: "POST",
			url: urlMetodo,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify(dados),
			beforeSend: beforeSend,
			success: callbackSucesso,
			error: callbackErro,
			complete: complete
		});
	}
</script>
