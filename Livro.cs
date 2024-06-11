using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    //Classe que cria um livro, ela possui um construtor que inicia o objeto criado por meio dos parametros fornecidos
    internal class Livro
    {
        public string Titulo { get; }
        public string Autor { get; }
        public int AnoPublicacao { get; }
        public int Paginas { get; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }

        public Livro(string titulo, string autor, int anoPublicacao, int paginas, int quantidade = 1, double valor = 0)
        {
            Titulo = titulo;
            Autor = autor;
            AnoPublicacao = anoPublicacao;
            Paginas = paginas;
            Valor = valor;
            Quantidade = quantidade;
        }
        //Exibe detalhes do livro com todas as informações
        public void ExibirDetalhesLivro()
        {
            Console.Clear();
            Console.WriteLine("------------------------");
            Console.WriteLine("  Informações do Livro  ");
            Console.WriteLine("------------------------");
            Console.WriteLine("Titulo: {0}", Titulo);
            Console.WriteLine("Autor: {0}", Autor);
            Console.WriteLine("Ano de Publicação: {0}", AnoPublicacao);
            Console.WriteLine("Paginas: {0}", Paginas);
            Console.WriteLine("Quantidade Disponível: {0}", Quantidade);
            Console.WriteLine("Valor R${0}", Valor);
            Console.WriteLine("------------------------");
        }
        
    }
}
