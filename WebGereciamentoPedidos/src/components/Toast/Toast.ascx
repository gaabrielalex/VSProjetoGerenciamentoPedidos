<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Toast.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.Toast.Toast" %>

<div id="container-toast-message" aria-live="polite" aria-atomic="true" class="d-flex justify-content-end align-items-end w-100;" style="position: fixed; top: 85px; right: 30px; z-index: 9999; overflow: hidden;">
	<div runat="server" id="toastMessage" class="toast my-toast-message" role="alert" aria-live="assertive" aria-atomic="true">
		<div class="toast-header">
			<%--<img src="" class="rounded me-2" alt="...">--%>
			<strong class="me-auto">Bootstrap</strong>
			<small>Agora</small>
			<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
		</div>
		<div class="toast-body">
			<!-- Mensagem será inserida aqui -->
		</div>
	</div>
</div>

<script type="text/javascript">
	function showToast(message, type) {
		var toastElement = document.querySelector('.my-toast-message');
		var toastHeader = document.querySelector('.my-toast-message .toast-header strong');
		var toastBody = document.querySelector('.my-toast-message .toast-body');

		toastBody.innerHTML = message;

		if (type === 's') {
			toastHeader.innerHTML = 'Sucesso';
			toastElement.className = 'toast bg-success text-white';
		} else if (type === 'e') {
			toastHeader.innerHTML = 'Erro';
			toastElement.className = 'toast bg-danger text-white';
		}

		var toast = new bootstrap.Toast(toastElement);
		toast.show();
	}
</script>
