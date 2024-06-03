<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoadingModal.ascx.cs" Inherits="WebGereciamentoPedidos.src.components.LoadingModal" %>

<div class="modal fade" id="loadingModal" tabindex="-1" aria-labelledby="loadingModalLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
	<div class="modal-dialog modal-dialog-centered">
		<div class="modal-content">
			<div class="modal-body text-center">
				<div class="spinner-border text-primary" role="status">
					<span class="sr-only"></span>
				</div>
				<p class="mt-3">Aguarde enquanto os dados estão sendo salvos...</p>
				<%--<div class="progress" role="progressbar" aria-label="Animated striped" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100">
					<div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 100%"></div>
				</div>--%>
			</div>
		</div>
	</div>
</div>

<script>
	function abrirLoadingModal() {
		$('#loadingModal').modal('show');
	}

	function fecharLoadingModal() {
		$('#loadingModal').modal('hide');
	}
</script>
