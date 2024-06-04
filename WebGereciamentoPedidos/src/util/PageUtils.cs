using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
			string script = "fecharLoadingModal()";
			ScriptManager.RegisterStartupScript(page, page.GetType(), "fecharLoadingModal", script, true);
		}

		public static void MostrarMensagemViaToast(string mensagem, TiposMensagem tipo, Page page)
		{	
			string script = $"showToast('{mensagem}', '{(char)tipo}');";
			ScriptManager.RegisterStartupScript(page, page.GetType(), "showToast", script, true);
		}
		public static void MostrarMensagemViaToastComDelay(string mensagem, TiposMensagem tipo, Page page)
		{
			string script = @"
				setTimeout(() => {
					showToast('" + mensagem + @"', '" + (char)tipo + @"');
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
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
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
	}
}