using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Services.Description;
using System.Web.UI;
using System.Windows.Forms;
using static WebGereciamentoPedidos.src.util.MensagemInfo;

namespace WebGereciamentoPedidos.src.util
{
	public class PageUtils
	{
		public static void FecharLoadingModal(Page page)
		{
			var script = "fecharLoadingModal()";
			ScriptManager.RegisterStartupScript(page, page.GetType(), "fecharLoadingModal", script, true);
		}

		public static void MostrarMensagemViaToast(string mensagem, TiposMensagem tipo, Page page)
		{	
			var script = $"myApp.showToast('{mensagem}', '{(char)tipo}');";
			ScriptManager.RegisterStartupScript(page, page.GetType(), "showToast", script, true);
		}
		public static void MostrarMensagemViaToastComDelay(string mensagem, TiposMensagem tipo, Page page)
		{
			var script = @"
				setTimeout(() => {
					myApp.showToast('" + mensagem + @"', '" + (char)tipo + @"');
				}, 500);
			";
			ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "showToast", script, true);
		}

		public static void MostrarMensagemViaAPISistemaOperacionalLocal(string mensagem, string tipo, Page page)
		{
			string titulo;

			if (tipo != "S" && tipo != "E")
			{
				throw new ArgumentException("Tipo inválido. Deve ser 'S' ou 'E'.");
			}
			if(tipo == "S")
			{
				tipo = "success";
				titulo = "Sucesso";
			}
			else
			{
				tipo = "error";
				titulo = "Erro";
			}

			MessageBoxButtons buttons = MessageBoxButtons.OK;
			MessageBox.Show(mensagem, titulo, buttons);
		}

		public static bool SolicitarConfirmacaoViaAPISistemaOperacionalLocal(string mensagem)
		{
			var buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show(mensagem, "Confirmação", buttons);
			if (result == DialogResult.Yes)
			{
				return true;
			}
			return false;
		}

		public static void RedirecionarClienteParaEvitarResubimissaoDeFormulario(HttpResponse response, HttpRequest request, HttpContext context) 
		{
			//Daí antes de redirecionar vc pode guardar o estado dos componentes e atributo
			//necessários da página para daí vc fazer o processod e redirecionamento
			response.Redirect(request.RawUrl, false);
			context.ApplicationInstance.CompleteRequest();
		}

		public static void RedirecionarParaPagina(Page page, HttpRequest request, string urlPagina, string MensagemAposRedirecionamento = "", TiposMensagem tipoMensagem = TiposMensagem.Sucesso)
		{
			RedirecionarParaPaginaComDelay(
				page: page,
				request: request,
				urlPagina: urlPagina,
				delay: 0,
				mensagemAposRedirecionamento: MensagemAposRedirecionamento,
				tipoMensagem: tipoMensagem
			);
		}

		public static void RedirecionarParaPaginaComDelay(Page page, HttpRequest request, string urlPagina, int delay = 1500, string mensagemAposRedirecionamento = "", TiposMensagem tipoMensagem = TiposMensagem.Sucesso)
		{
			urlPagina = page.ResolveUrl(urlPagina);

			var script = $@"
					setTimeout(function() {{
						window.location.href = '{urlPagina}';
					}}, {delay});";

			if(!string.IsNullOrEmpty(mensagemAposRedirecionamento))
			{
				AdicionarMensagemParaSerExibidaNoProximoCarregamentoDePagina(page, mensagemAposRedirecionamento, tipoMensagem);
			}
			ScriptManager.RegisterStartupScript(page, page.GetType(), "redirecionarComDelay", script, true);
		}

		public static void AdicionarMensagemParaSerExibidaNoProximoCarregamentoDePagina(Page page, string mensagem, TiposMensagem tipo)
		{
			var script = $"myApp.ServicoMensagensAoCarregarPaginas.adicionarMensagem('{mensagem}', '{(char)tipo}');";
			ScriptManager.RegisterStartupScript(page, page.GetType(), "mensagemLocalStorage", script, true);
		}

	}
}