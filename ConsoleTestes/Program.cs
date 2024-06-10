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
			Cliente cliente = new Cliente(1);

			Console.Write(cliente.ToString());
		}

		public class Cliente
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int Idade { get; set; }


			public Cliente(int id)  : this(id, "djoqwjho", 19999) 
			{
					
			}

			public Cliente(int id, string nome, int idade)
			{
				Id = id;
				Name = nome;
				Idade = idade;
			}

			public override string ToString()
			{
				var str = $"Id: {Id}, Nome: {Name}, Idade: {Idade}";
				return str;
			}
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
