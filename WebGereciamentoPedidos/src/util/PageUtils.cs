using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;

namespace WebGereciamentoPedidos.src.util
{
	public class PageUtils
	{
		//Tipos válidos: S (sucesso) e E (erro)
		public static void mostrarMensagem(string mensagem, string tipo, System.Web.UI.Page page)
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

			//Não funciona por enquanto por esse método
			//string script = $"showToast('{mensagem}', '{tipo}');";
			//page.ClientScript.RegisterClientScriptBlock(typeof(Page), "showToast", script, true);

			//Não funciona tb
			//string script = "$('#modalMessage').modal('show'); ";
			//page.ClientScript.RegisterStartupScript(typeof(Page), "showModalMessage", script, true);

			//Não funciona tb
			//page.ClientScript.RegisterStartupScript(typeof(Page), "showModalMessage", "alert('teste');", true);

			//Funciona
			MessageBoxButtons buttons = MessageBoxButtons.OK;
			MessageBox.Show(mensagem, titulo, buttons);
		}

		public static bool solicitarConfirmacao(string mensagem)
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
	}
}