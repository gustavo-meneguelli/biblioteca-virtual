using Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
internal class AcervoLivro
    {
        public Dictionary<int, Livro> ListaLivros {  get; set; }
        public AcervoLivro()
        {
            //Lista de livros 
            ListaLivros = new Dictionary<int, Livro> {
            { 1, new Livro("Orgulho e Preconceito", "Jane Austen", 1813, 432, 1, 46.99)  },
            { 2, new Livro("It", "Stephen King", 1980, 1138, 4, 34.77) },
            { 3, new Livro("1984", "George Orwell", 1949, 328, 5, 32) },
            { 4, new Livro("Moby Dick", "Herman Melville", 1851, 585, 2, 29.99) }
            };
        }
        //MenuBiblioteca do Acervo de Livros
        public void MenuBiblioteca()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("     Acervo de Livros   ");
            Console.WriteLine("------------------------");
            Console.WriteLine("[1] Catálogo");
            Console.WriteLine("[2] Adicionar Livros (Não implementado)");
            Console.WriteLine("[3] Remover Livros (Não implementado)");
            Console.WriteLine("[0] Sair");

        }
        //Catálogo do Acervo de Livros
        public void ExibirCatalogo()
        {
            foreach (var indice in ListaLivros.Keys)
            {
                Console.WriteLine("[{0}] {1}", indice, ListaLivros[indice].Titulo);
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("[0] Voltar ao Menu Principal");
            Console.WriteLine("----------------------------");
        }
        //Adicionar livros dinamicamente
        public Livro AdicionarLivro()
        {
            ValidadorDados validacao = new ValidadorDados();
            MenuBiblioteca menu = new MenuBiblioteca(); 

            // Variavéis são declaradas com valores inválidos para garantir o loop
            string tituloLivro = null;
            string autorLivro = null;
            int anoPublicacaoLivro = 0;
            int quantidadePaginasLivro = 0;
            int quantidadeLivros = 0;
            double valorLivro = 0.00;
            bool todasEntradasValidas = false;

            while (!todasEntradasValidas) // Loop encerra quando todas as entradas para as propriedades do livro são válidas
            {
                menu.ExibirMenuAdicionarLivro(tituloLivro, autorLivro, anoPublicacaoLivro, quantidadePaginasLivro, quantidadeLivros, valorLivro); //Exibe o menu principal do Menu

                //Adicionar todas as propriedades do livro, caso as variáveis sejam inválidas o loop permanece
                if (string.IsNullOrEmpty(tituloLivro))
                {
                    tituloLivro = validacao.ValidarTituloLivro();
                }
                else if (string.IsNullOrEmpty(autorLivro))
                {
                    autorLivro = validacao.ValidarAutorLivro();
                }
                else if (anoPublicacaoLivro == 0)
                {
                    anoPublicacaoLivro = validacao.ValidarAnoPublicacaoLivro();
                }
                else if (quantidadePaginasLivro == 0)
                {
                    quantidadePaginasLivro = validacao.ValidarQuantidadePaginasLivro();
                }
                else if (quantidadeLivros == 0)
                {
                    quantidadeLivros = validacao.ValidarQuantidadeUnidadesLivro();
                }
                else if (valorLivro == 0)
                {
                    valorLivro = double.Parse(validacao.ValidarValorLivro().ToString("F2"));
                }
                else
                {
                    todasEntradasValidas = true; // Todas as entradas são válidas
                    Console.WriteLine("Livro adicionado com sucesso");
                    Console.WriteLine("----------------------------");
                    Console.ReadKey();
                }
            }
            return new Livro(tituloLivro, autorLivro, anoPublicacaoLivro, quantidadePaginasLivro, quantidadeLivros, valorLivro);
        }
        //Remover livros dinamicamente
        public void RemoverLivro()
        {
            ValidadorDados validacao = new ValidadorDados();
            string inputUsuario = "";
            do
            {
                Console.Clear();
                Console.WriteLine("--------------------------");
                Console.WriteLine("      Remover Livro");
                Console.WriteLine("--------------------------");

                ExibirCatalogo();
                Console.Write("Opção Desejada: ");
                inputUsuario = Console.ReadLine();
            } while (!validacao.ValidarEntradaUsuario("Opção desejada", inputUsuario, true, false, ListaLivros.Count)); // Valida a entrada do usuário
            

            var listaAtualizada = new Dictionary<int, Livro>(); // cria uma nova lista 

            int novaChave = 1;

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("{0} removido com sucesso!", ListaLivros[int.Parse(inputUsuario)].Titulo);
            ListaLivros.Remove(int.Parse(inputUsuario)); // Remove o livro baseado no indice que o usuário escolheu

            foreach (var livro in ListaLivros.Values)
            {
                listaAtualizada.Add(novaChave, livro); // Atualiza os indices adicionando os livros restantes na nova lista
                novaChave++;
            }
            ListaLivros = listaAtualizada; // Lista original recebe a lista atualizada
            Console.ReadKey();

        }
        //Exibi informações do livro
        public void ExibirDetalhesDoLivro(int indiceLivro)
        {
            ListaLivros[indiceLivro].ExibirDetalhesLivro();
        }
        //Seleciona o Livro que o usuario escolher
        public Livro ObterLivroPorTitulo(int indiceLivro)
        {
            return ListaLivros[indiceLivro];
        }
        //Atualiza o Estoque do Livro
        public Livro DiminuirEstoque(Livro livro)
        {
            livro.Quantidade--;
            return livro;
        }
    }
}
