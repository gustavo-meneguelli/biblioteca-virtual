using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    // Cria um usuario com alguns valores padrões, essa classe possui um construtor que é inicado com os parametros fornecidos.
    internal class Usuario
    {
        Random random = new Random();
        public string Nome { get; set; }
        private int _id;
        public int Id { get { return _id; } 
            set 
            {
                //Gerar um id aleatório com 5 digitos
                string numeroAleatorio = "";
                for (int i = 0; i < 5; i++)
                    numeroAleatorio += random.Next(0, 10).ToString();
                _id = int.Parse(numeroAleatorio);
            } }
        public double SaldoDinheiro { get; set; }
        public double LimiteCartaoCredito { get; set; }
        public List<List<object>> ListaComprasUsuario { get; set; }
        public Usuario(string nome)
        {
            Nome = nome;
            SaldoDinheiro = 59.99;
            LimiteCartaoCredito = 100;
            Id = _id;
            ListaComprasUsuario = new List<List<object>>();
        }
        public Usuario()
        {
            Nome = "Visitante";
            SaldoDinheiro = 0;
            LimiteCartaoCredito = 0;
            Id = 0;
            ListaComprasUsuario = new List<List<object>>();

            Console.WriteLine("Usuario logado: {0}", Nome);
        }
        //Exibe na tela do console as informações do usuario logado
        public void ExibirInformacao()
        {
            Console.WriteLine("Usuario logado: {0}              Saldo em Dinheiro R${1}", Nome, SaldoDinheiro.ToString("F2"));
            Console.WriteLine("Id: {0}                            Cartão de Crédito R${1}", Id, LimiteCartaoCredito.ToString("F2"));
        }
        //Exibe a lista de compras realizadas pelo usuario com detalhes
        public void ExibirListaCompras()
        {
            if (ListaComprasUsuario.Count == 0)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("Nenhuma compra realizada");
                Console.WriteLine("------------------------");
            }
            else
            {
                //Exibe o MenuBiblioteca no console
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("                 Compras Realizadas por {0}", Nome);
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Titulo     Valor R$      Forma de Pagamento      Id      Data da Compra");
                Console.WriteLine("-----------------------------------------------------------------------");

                foreach (var lista in ListaComprasUsuario) // Loop que passa pela lista de compras
                {
                    foreach (var item in lista) // Loop que passa pelos itens de cada lista de compra do usuário
                    {
                        if (lista.IndexOf(item) == lista.Count - 1) // Verifica se o item é o ultimo
                            Console.Write(item.ToString());
                        else
                            Console.Write(item.ToString() + ", "); // Se não for o ultimo adiciona uma virgula
                    }
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------------------------------");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
