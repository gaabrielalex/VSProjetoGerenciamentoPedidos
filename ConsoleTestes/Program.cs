using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestes
{
	internal class Program
	{
		static void Main(string[] args)
		{

			//ItemDAO itemDAO = new ItemDAO();

			//itemDAO.obterPorDescricao();
			////Não tem acesso
			//itemDAO.ObterPorId();

			//IDAO<Item> itemDAO2 = new ItemDAO();
			//itemDAO2.ObterPorId();
			//itemDAO2.ObterPorDescricao();

			//ItemDAO itemDAO3 = new IDAO<Item>();
			//itemDAO3.obterPorDescricao();

			//PessoaDAO pessoaDAO = new PessoaDAO();
			//pessoaDAO.ObterPorId();



		}

		public interface IDAO<T> where T : class
		{
			T ObterPorId();

		}

		public class Item
		{
			public string descricao { get; set; }
		}

		public class ItemDAO : IDAO<Item>
		{
			Item IDAO<Item>.ObterPorId()
			{
				return new Item() { descricao = "Teste123" };
			}

			public Item obterPorDescricao()
			{
				return new Item() { descricao = "testeDesfricao" };
			}
		}

		public class Pessoa
		{

		}

		public class PessoaDAO : IDAO<Pessoa>
		{
			public Pessoa ObterPorId()
			{
				return new Pessoa();
			}

			public Pessoa ObterPorDataNascimento()
			{
				return new Pessoa();
			}
		}
	}
}
