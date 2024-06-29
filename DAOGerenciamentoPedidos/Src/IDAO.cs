using DAOGerenciamentoPedidos.Src.Data_Base;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAOGerenciamentoPedidos.Src
{
	public interface IDAO<T> where T : class
	{
		int Inserir(T obj);

		void Editar(T obj, int idObjASerEditado);

		void Excluir(int id);

		IList<T> ListarTodos();

		T ObterPorId(int id);

		IList<T> ConverterReaderParaListaDeObjetos(SqlDataReader reader);
	}
}