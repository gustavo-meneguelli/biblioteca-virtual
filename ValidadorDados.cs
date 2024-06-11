using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Biblioteca
{
    internal class ValidadorDados
    {
        //Valida todas as entradas para o método Adicionar Livro (Talvez sirva para outro método no futuro)
        public bool ValidarEntradaUsuario(string nomePropriedade, string entrada, bool aceitarNumeros = true, bool aceitarTexto = true, int maxOpcoesMenu = 0)
        {
            
            Regex regex = new Regex(".*[A-Za-z0-9].*"); // Verificar se existe um caracter alfanúmerico e não apenas simbolos
            //Lista de propriedades que podem conter apenas 1 dígito                                            
            List<string> listaPodeUmaLetraValida = new List<string> { "Quantidade de páginas", "Opção desejada", "Quantidade de livros" };

            // Verifica se a entrada está vazia ou não contém letras ou números
            if ((string.IsNullOrEmpty(entrada)) || (!regex.IsMatch(entrada))) 
            {
                Console.WriteLine("Error, o {0} não pode conter espaços vazios ou apenas símbolos. ", nomePropriedade);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada possue apenas letras
            else if (aceitarTexto && !aceitarNumeros && int.TryParse(entrada, out _)) 
            {
                Console.WriteLine("Error! {0} não pode conter números...", nomePropriedade);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada possue apenas 1 letra quando não é permitido
            else if (entrada.Length == 1 && !listaPodeUmaLetraValida.Contains(nomePropriedade)) 
            {
                Console.WriteLine("Error! {0} não pode conter apenas 1 caractere... Digite um valor válido", nomePropriedade);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada a entrada digita possue mais que 20 caracteres que é o limite
            else if (entrada.Length > 20)
            {
                Console.WriteLine("Error... {0} não pode conter mais que 20 caracteres...", nomePropriedade);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada contém apenas numeros digitados
            else if (aceitarNumeros && !aceitarTexto && !double.TryParse(entrada, out _)) 
            {
                Console.WriteLine("Error! {0} deve conter apenas números válidos...", nomePropriedade);
                Console.ReadKey();
                return false;   
            }
            //Verifica se a entrada o número digitado é menor o igual a zero (a propriedade "Opção desejada pode possuir a entrada como 0)
            else if ((aceitarNumeros && !aceitarTexto) && (int.TryParse(entrada, out int valorInt) && (valorInt <= 0) && (nomePropriedade != "Opção desejada"))) //Verifica se a entrada o número é menor ou igual a zero
            {
                Console.WriteLine("Error! {0} não pode ser menor ou igual ao número 0.", nomePropriedade);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada for a nomePropriedade ela precisa ser menor que o ano vigente
            else if (nomePropriedade == "Ano de publicação" && int.TryParse(entrada, out valorInt) && valorInt > DateTime.Now.Year) 
            {
                Console.WriteLine("Error! {0} não pode ser maior que o ano vigente de {1}.", nomePropriedade, DateTime.Now.Year);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada a nomePropriedade possue o valorInt maioe que 20.000
            else if (nomePropriedade == "Quantidade de páginas" && int.TryParse(entrada, out valorInt) && valorInt > 20000) 
            {
                Console.WriteLine("Error! {0} não pode ser maior que o 20.000 páginas.", nomePropriedade);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada a nomePropriedade possue o valor maior que 100
            else if (nomePropriedade == "Quantidade de livros" && int.TryParse(entrada, out valorInt) && valorInt > 100) 
            {
                Console.WriteLine("Error! {0} não pode ser maior que o 100 unidades.", nomePropriedade);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada a opção escolhida na entrada está entre 0 e a quantidade máxima de opões do menu
            else if (nomePropriedade == "Opção desejada" && int.TryParse(entrada, out valorInt) && valorInt > maxOpcoesMenu)
            {
                Console.WriteLine("Error. Escolha uma opção entre 0 e {0}", maxOpcoesMenu);
                Console.ReadKey();
                return false;
            }
            //Verifica se a entrada o valor digitado pode ser convertido para double e está e abaixo do valor máximo permitido
            else if (nomePropriedade == "Valor" && double.TryParse(entrada, out double valorDouble) && valorDouble > 99.99)
            {
                if (entrada.Contains("."))
                {
                    Console.WriteLine("Utilize vírgula ou invés de pontos");
                    Console.WriteLine("Exemplo: 19,00");
                }
                else
                {
                    Console.WriteLine("{0} não pode ser maior que R$99.99 de acordo com as diretrizes da biblioteca", nomePropriedade);

                }
                Console.ReadKey();
                return false;
            }
            //Caso contrário a opção é válida
            else
            {
                return true;
            }
        }
        public string ValidarTituloLivro() // Adiciona titulo no método AdicionarLivro em Acervo
        {
            Console.Write("Titulo: ");
            string entradaUsuario = Console.ReadLine();

            return ValidarEntradaUsuario("Titulo", entradaUsuario, true, true) ? entradaUsuario.Trim() : string.Empty ;
            
        }
        public string ValidarAutorLivro() // Adiciona autor no método AdicionarLivro em Acervo
        {
            Console.Write("Autor: ");
            string entradaUsuario = Console.ReadLine();

            return ValidarEntradaUsuario("Autor", entradaUsuario, false, true) ? entradaUsuario.Trim() : string.Empty ;

        } 
        public int ValidarAnoPublicacaoLivro() // Adiciona Ano da publicaçãp no método AdicionarLivro em Acervo
        {
            Console.Write("Ano de Publicação: ");
            string anoPublicacaoString = Console.ReadLine();

            return ValidarEntradaUsuario("Ano de publicação", anoPublicacaoString, true, false) ? int.Parse(anoPublicacaoString) : 0;
        }
        public int ValidarQuantidadePaginasLivro() // Adiciona Páginas no método AdicionarLivro em Acervo
        {
            Console.Write("Quantidade de Páginas (limite 20.000): ");
            string quantidadePaginas = Console.ReadLine();

            return ValidarEntradaUsuario("Quantidade de páginas", quantidadePaginas, true, false) ? int.Parse(quantidadePaginas) : 0;
        }
        public int ValidarQuantidadeUnidadesLivro() // Adiciona Unidades de livros no método AdicionarLivro em Acervo
        {
            Console.Write("Quantidade de livros (limite 100): ");
            string quantidadeLivros = Console.ReadLine();

            return ValidarEntradaUsuario("Quantidade de livros", quantidadeLivros, true, false) ? int.Parse(quantidadeLivros) : 0;
        }
        public double ValidarValorLivro() // Adiciona o valor do livro em Acervo
        {
            Console.Write("Valor do livro R$");
            string valorLivros = Console.ReadLine();

            return ValidarEntradaUsuario("Valor", valorLivros, true, false) ? double.Parse(valorLivros) :0;
        } 

    }
}
