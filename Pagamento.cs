using System;
using System.Collections.Generic;

namespace Biblioteca
{
    //Formas de Pagamento Disponíveis
    enum FormaPagamento
    {
        Dinheiro,
        CartaoCredito
    }

    // Classe para realizar o pagamento
    internal class Pagamento
    {
        private static Random random = new Random();
        private DateTime DataCompra = DateTime.Now; //Define a Data da Compra quando o método Pagar for chamado
        private int _id; //Recebe o Id com 8 números aleatórios
        public int Id
        {
            get { return _id; }
            private set
            {
                // Gerar um id aleatório com 8 dígitos
                string numeroAleatorio = "";
                for (int i = 0; i < 8; i++)
                    numeroAleatorio += random.Next(0, 10).ToString();
                _id = int.Parse(numeroAleatorio);
            }
        } 
        protected FormaPagamento FormaPagamento { get; set; } //Enum com as formas de pagamento

        // Construtor para inicializar o Id
        public Pagamento()
        {
            Id = 0; // Isso acionará o set e gerará um número aleatório
        }
        // Método para processar o pagamento
        public virtual void Pagar(Usuario usuario, Livro livro)
        {
            //Adicionando a transação completa na lista de compras do usuário
            usuario.ListaComprasUsuario.Add(new List<object> { livro.Titulo, livro.Valor, FormaPagamento, Id, DataCompra });

            // Método para imprimir a nota fiscal do usuário
            ImprimirNotaFiscal(usuario, livro);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para voltar....");
        }
        // Imprime a nota fiscal com as informações da compra
        private void ImprimirNotaFiscal(Usuario usuario, Livro livro)
        {
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("| Pagamento realizado com sucesso |");
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("Nome : {0}\nId: {1}\nLivro: {2}\nValor R${3}", usuario.Nome, Id, livro.Titulo, livro.Valor);
        }
        // Método para verificar o saldo e retornar verdadeiro ou falso para verificar se o usuário possui saldo.
        protected bool VerificarSaldo(double valorSaldo, double valorLivro)
        {
            return valorSaldo >= valorLivro;
        }
        // Atualiza o saldo do usuário
        protected double AtualizarSaldo(double valorSaldo, double valorLivro)
        {
            return valorSaldo - valorLivro;
        }
        // Verificação do estoque de livros
        protected bool VerificarEstoque(Livro livro)
        {
            if (livro.Quantidade < 1)
                Console.WriteLine("Livro esgotado!");
            return livro.Quantidade > 0;
        }
    }
}
