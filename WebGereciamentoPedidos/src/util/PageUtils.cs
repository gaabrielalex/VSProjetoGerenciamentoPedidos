using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Windows.Forms;

namespace WebGereciamentoPedidos.src.util
{
	public class PageUtils
	{
		//Tipos válidos: S (sucesso) e E (erro)
		public static void MostrarMensagemViaToast(string mensagem, string tipo, System.Web.UI.Page page)
		{	
			tipo = tipo.ToLower();
			if (tipo != "s" && tipo != "e")
			{
				throw new ArgumentException("Tipo inválido. Deve ser 'S' ou 'E'.");
			}

			string script = $"showToast('{mensagem}', '{tipo}');";
			ScriptManager.RegisterStartupScript(page, page.GetType(), "showToast", script, true);
		}

		public static void MostrarMensagemViaAPISistemaOperacionalLocal(string mensagem, string tipo, System.Web.UI.Page page)
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