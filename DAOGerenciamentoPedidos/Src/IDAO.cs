using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOGerenciamentoPedidos.Src
{
	public interface IDAO<T> where T : class
	{
		int Inserir(T obj);

		void Editar(T obj, int idObjASerEditado);

		void Excluir(int id);

		List<T> Listar();

		T ObterPorId(int id);

		List<T> ConverterReaderParaListaDeObjetos(SqlDataReader reader);
	}
}
