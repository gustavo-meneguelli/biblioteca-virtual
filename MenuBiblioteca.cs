using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Biblioteca
{
    internal class MenuBiblioteca
    {
        //Exibe na tela as opções iniciais do programa
        public void ExibirMenuPrincipal()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("        Biblioteca      ");
            Console.WriteLine("------------------------");
            Console.WriteLine("[1] Catálogo");
            Console.WriteLine("[2] Adicionar Livros");
            Console.WriteLine("[3] Remover Livros");
            Console.WriteLine("[4] Consultar Compras Realizadas");
            Console.WriteLine("------------------------");
            Console.WriteLine("[0] Encerrar programa...");
            Console.WriteLine("------------------------");
        }
        //Exibe na tela o menu das opções sobre o livro escolhido
        public void ExibirMenuLivro(Livro livroUsuario)
        {
            Console.Clear();
            Console.WriteLine("Quantidade em Estoque: {0}", livroUsuario.Quantidade);
            Console.WriteLine("----------------------------");
            Console.WriteLine("Livro: {0}                ", livroUsuario.Titulo);
            Console.WriteLine("----------------------------");
            Console.WriteLine("[1] Exibir Detalhes do Livro");
            Console.WriteLine("[2] Comprar Livro");
            Console.WriteLine("----------------------------");
            Console.WriteLine("[0] Voltar ao Menu Principal");
            Console.WriteLine("----------------------------");
        }
        //Processa a lógica das opções no catálogo
        public void ProcessarMenuCatalogo(Usuario usuario, AcervoLivro acervo)
        {
            ValidadorDados validacao = new ValidadorDados();
            string opcaoMenuBibliotecaCatalogo = "";
            do
            {
                Console.Clear();

                usuario.ExibirInformacao(); // Exibe informações do usuário logado no console
                Console.WriteLine("----------------");
                Console.WriteLine("     Catálogo   ");
                Console.WriteLine("----------------");

                acervo.ExibirCatalogo(); //Exibe as informações do menu de catálogo
                Console.Write("Opção desejada: ");
                opcaoMenuBibliotecaCatalogo = Console.ReadLine();

            } while (!validacao.ValidarEntradaUsuario("Opção desejada", opcaoMenuBibliotecaCatalogo, true, false, acervo.ListaLivros.Count)); //Loop continua se a condição for falsa
            
            if (VerificarDisponibilidadeLivro(acervo, opcaoMenuBibliotecaCatalogo) && opcaoMenuBibliotecaCatalogo != "0") // Verifica se o livro está disponível e se a opção não é "0" (condição de volta ao menu principal)
            {
                Livro livroUsuario = acervo.ObterLivroPorTitulo(int.Parse(opcaoMenuBibliotecaCatalogo)); // Criando o Livro baseado na escolha do usuario
                
                string opcaoMenuBibliotecaLivro = ""; // Variável criada antes para garantir o loop
                do
                {
                    ExibirMenuLivro(livroUsuario); // Exibindo o menu do livro escolhido
                    Console.Write("Opção desejada: ");
                    opcaoMenuBibliotecaLivro = Console.ReadLine();

                } while (!validacao.ValidarEntradaUsuario("Opção desejada", opcaoMenuBibliotecaLivro, true, false, 2));

                switch (opcaoMenuBibliotecaLivro)
                {
                    case "1": //Opção Exibir Detalhes do Livro
                        {
                            livroUsuario.ExibirDetalhesLivro();
                            Console.WriteLine("Pressione qualquer tecla para voltar...");
                            Console.ReadKey();
                            break;
                        }
                    case "2": // Opção Comprar Livro
                        {
                            ProcessarFormaPagamento(usuario, livroUsuario);
                            break;
                        }
                }
            }
        }
        //Exibi o MenuBiblioteca com as formas de pagamento disponíveis
        public void ExibirMenuFormaPagamento(Livro livro)
        {
            Console.Clear();
            Console.WriteLine("Livro: {0}           Valor R${1}", livro.Titulo, livro.Valor);
            Console.WriteLine("----------------------------");
            Console.WriteLine("     Forma de Pagamento     ");
            Console.WriteLine("----------------------------");
            Console.WriteLine("[1] Cartão de Crédito");
            Console.WriteLine("[2] Dinheiro");
            Console.WriteLine("----------------------------");
            Console.WriteLine("[0] Voltar ao Menu Principal");
            Console.WriteLine("----------------------------");
        }
        //Retorna a pagamento que o usuario escolher
        public void ProcessarFormaPagamento(Usuario usuario, Livro livro)
        {
            ValidadorDados validacao = new ValidadorDados();
            //Criando os objetos de Pagamentos
            Pagamento pagarDinheiro = new PagamentoDinheiro();
            Pagamento pagarCartao = new PagamentoCartao();
            AcervoLivro acervo = new AcervoLivro();

            string metodoPagamentoSelecionado = "";

            //Laço que exibe o menu e valida a entrada do usuário até que a entrada esteja válida
            do
            {
                ExibirMenuFormaPagamento(livro);

                if (metodoPagamentoSelecionado == "0")
                    ProcessarMenuCatalogo(usuario, acervo);

                Console.Write("Opção desejada: ");
                metodoPagamentoSelecionado = Console.ReadLine();

            } while (!validacao.ValidarEntradaUsuario("Opção desejada", metodoPagamentoSelecionado, true, false, 2));

            Console.Clear();
            if (metodoPagamentoSelecionado != "0") // A estrutura inicia se a opção não for "0" que seria voltar ao menu
            {

                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(metodoPagamentoSelecionado == "1" ? "(Selecionado Crédito)" : "(Selecionado Dinheiro)");
                Console.WriteLine("Pressione qualquer tecla para finalizar a compra...");
                Console.WriteLine("---------------------------------------------------");
                Console.ReadKey();

                //Estrutura de controle para obter a forma de pagamento baseada na escolha do usuario
                switch (metodoPagamentoSelecionado)
                {
                    case "1":
                        pagarCartao.Pagar(usuario, livro); break; //Cartão de crédito
                    case "2":
                        pagarDinheiro.Pagar(usuario, livro); break; //Dinheiro
                }
                Console.ReadKey();
            }
        }
        //Exibe o menu do método Adicionar Livros (Classe: AcervoLivro)
        public void ExibirMenuAdicionarLivro(string tituloLivro, string autorLivro, int anoPublicacaoLivro, int quantidadePaginasLivro, int quantidadeLivros, double valorLivro)
        {
            Console.Clear();
            Console.WriteLine("------------------------");
            Console.WriteLine("     Adicionar Livros   ");
            Console.WriteLine("------------------------");
            Console.WriteLine("Titulo: {0}, Autor: {1}, Ano de Publicação: {2}, Páginas: {3}, Quantidade: {4}, Valor R${5}",
                tituloLivro, autorLivro, anoPublicacaoLivro, quantidadePaginasLivro, quantidadeLivros, valorLivro.ToString("F2"));
            Console.WriteLine("------------------------");
        }

        //Verifica se o indice do livro escolhido está dentro da lista de livros
        private bool VerificarDisponibilidadeLivro(AcervoLivro acervo, string indiceLivro)
        {
            return acervo.ListaLivros.ContainsKey(int.Parse(indiceLivro)) ? true: false;
        }
       
    }
}
