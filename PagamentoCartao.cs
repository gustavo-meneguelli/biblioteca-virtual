using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class PagamentoCartao : Pagamento
    {
        //Inicializando o construtor para garantir que a forma de pagamento seja adequada
        public PagamentoCartao() 
        {
            FormaPagamento = FormaPagamento.CartaoCredito;
        }

        //Método sobrescrito para processar o pagamento em cartão
        public override void Pagar(Usuario usuario, Livro livro)
        {
            //base.FormaPagamento = "Cartão de Crédito";

            Console.WriteLine("Forma de Pagamento: Cartão de Crédito");
            //Condição para verificar o saldo do usuario e verificar se tem limite no cartão

            if (base.VerificarSaldo(usuario.LimiteCartaoCredito, livro.Valor) && base.VerificarEstoque(livro))
            {
                //Atualiza o limite do cartão de crédito do usuario
                usuario.LimiteCartaoCredito = base.AtualizarSaldo(usuario.LimiteCartaoCredito, livro.Valor);
                // Realiza o pagamento
                base.Pagar(usuario, livro); 
                //Retira do estoque a unidade comprada
                livro.Quantidade--;

            }
            else if (usuario.LimiteCartaoCredito < livro.Valor)
            {
                //Exibi uma mensagem de erro caso o saldo for menor que o valor do livro comprado
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Pagamento não realizado por falta de limite no cartão, consulte seu limite");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.ReadKey();
            }
        }
        
    }
}
