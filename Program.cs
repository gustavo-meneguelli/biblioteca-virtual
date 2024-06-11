using System.ComponentModel.Design;

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Inicializando objetos necessários para o funcionamento do programa
            Pagamento pagamento = new Pagamento();
            Pagamento pagarDinheiro = new PagamentoDinheiro();
            Pagamento pagarCartao = new PagamentoCartao();
            Usuario usuario = new Usuario("Gustavo");
            AcervoLivro acervo = new AcervoLivro();
            MenuBiblioteca menu = new MenuBiblioteca();
            ValidadorDados validacao = new ValidadorDados();

            //Loop principal do programa (Condição de saida é = "0")
            while (true)
            {
                Console.Clear();
                string opcaoMenuBibliotecaPrincipal = "";

                do
                {
                    Console.Clear();
                    usuario.ExibirInformacao(); // Exibe informações do usuário logado no console
                    menu.ExibirMenuPrincipal(); //Exibe o menu com as opções disponíveis
                    Console.Write("Opção desejada: ");
                    opcaoMenuBibliotecaPrincipal = Console.ReadLine();
                } while (!validacao.ValidarEntradaUsuario("Opção desejada", opcaoMenuBibliotecaPrincipal, true, false, 4)); //Válida a entrada do usuário

                switch (opcaoMenuBibliotecaPrincipal) 
                {
                    //Opção Catálogo de livros
                    case "1":
                        {
                            Console.Clear();
                            menu.ProcessarMenuCatalogo(usuario, acervo); // Processa a opção escolhida no cátalogo
                            break;
                        }
                    case "2":
                        {
                            //Adicionar Livros
                            acervo.ListaLivros.Add(acervo.ListaLivros.Count + 1, acervo.AdicionarLivro());
                            break;
                        }
                    case "3":
                        {
                            //Remover Livros
                            acervo.RemoverLivro();
                            break;
                        }
                    case "4":
                        {
                            //Exibir as compras do usuário com detalhes
                            Console.Clear();
                            usuario.ExibirListaCompras();
                            break;
                        }
                    case "0":
                        {
                            //Sair do programa
                            Console.WriteLine("|-------------------|");
                            Console.WriteLine("|Programa Encerrado!|");
                            Console.WriteLine("|-------------------|");
                            Environment.Exit(0);
                            break;
                            
                        }
                }
            }
            
        }
    }
}
