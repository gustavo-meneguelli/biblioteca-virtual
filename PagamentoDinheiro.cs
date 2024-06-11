using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class PagamentoDinheiro : Pagamento
    {
        //Inicializando o construtor para garantir que a forma de pagamento seja adequada
        public PagamentoDinheiro() 
        {
            FormaPagamento = FormaPagamento.Dinheiro;
        }
        //Método sobrescrito para processar o pagameto em dinheiro
        public override void Pagar(Usuario usuario, Livro livro)
        {
            Console.WriteLine("Forma de Pagamento: Dinheiro");

            //Condição para verificar o saldo do usuario e verificar se tem saldo em dinheiro e verifica a quantidade no estoque
            if (base.VerificarSaldo(usuario.SaldoDinheiro, livro.Valor) && base.VerificarEstoque(livro)) 
            {
                base.Pagar(usuario, livro);
                //Atualiza o saldo em dinheiro do usuario
                usuario.SaldoDinheiro = base.AtualizarSaldo(usuario.SaldoDinheiro, livro.Valor);
                //Retira do estoque a unidade comprada
                livro.Quantidade--;
            }
            else if (usuario.SaldoDinheiro < livro.Valor)
            {
                //Exibi uma mensagem de erro caso o saldo for menor que o valor do livro comprado
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Pagamento não realizado por falta de saldo em dinheiro, verifique seu saldo...");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.ReadKey();
            }

        }
    }
}
