using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas= new List<Pessoa>();
        private static int opcao = 0;
        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.WriteLine("                                              ");
            Console.WriteLine("           Digite a Opcao desejada :          ");
            Console.WriteLine("           =============================      ");
            Console.WriteLine("            1 - Criar Conta                   ");
            Console.WriteLine("           =============================      ");
            Console.WriteLine("            2 - Entrar com CPF e Senha        ");
            Console.WriteLine("           =============================      ");
            Console.WriteLine("            3 - Sair                          ");
            Console.WriteLine("           =============================      ");

            opcao = int.Parse(Console.ReadLine());
            switch(opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaLogin();
                    break;
                case 3:
                    Console.WriteLine("Obrigado por ter usado o programa!");
                    break;
                default: 
                    Console.Write("Opcao invalida");
                    break;
            }

        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                              ");
            Console.WriteLine("           Digite seu nome:                   ");
            string nome = Console.ReadLine();
            Console.WriteLine("           =============================      ");
            Console.WriteLine("           Digite o CPF                       ");
            string cpf = Console.ReadLine();
            Console.WriteLine("           =============================      ");
            Console.WriteLine("           Digite sua senha                   ");
            string senha = Console.ReadLine();
            Console.WriteLine("           =============================      ");

            //Criar uma Conta
            ContaCorrente contaCorrente= new ContaCorrente();
            Pessoa pessoa= new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);
            Console.Clear();
            Console.WriteLine("Conta cadastrada com sucesso!"); 

            //Espera 1 segundo para ir a outra tela
            Thread.Sleep(1000);
            TelaContaLogada(pessoa);


        }

        private static void TelaLogin()
        {
            Console.Clear();

            Console.WriteLine("                                              ");
            Console.WriteLine("           Digite o CPF:                      ");
            string cpf = Console.ReadLine();
            Console.WriteLine("           =============================      ");
            Console.WriteLine("           Digite a sua SENHA:                ");
            string senha = Console.ReadLine();
            //Logar no sistema

            Pessoa pessoa= pessoas.FirstOrDefault(x => x.CPF==cpf && x.Senha==senha);
            if (pessoa != null)
            {
                //Tela de boas Vindas
                TelaBoasVindas(pessoa);
                Console.Write("");
                //Tela conta logada
                TelaContaLogada(pessoa);
            }
            else
            {
                Console.Clear() ;
                Console.WriteLine("             Pessoa nao cadastrada");
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            string msgTelaBemVindo =
                $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDoBanco()} | Agencia: {pessoa.Conta.GetNumeroAgencia()} | Conta: {pessoa.Conta.GetNumeroDaConta()}";
            Console.WriteLine("");
            Console.WriteLine($"        Seja bem-vindo, {msgTelaBemVindo}");
        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);
            Console.WriteLine("             Digite a opcao desejada:     ");
            Console.WriteLine("             =============================");
            Console.WriteLine("             1- Realizar um Deposito:     ");
            Console.WriteLine("             =============================");
            Console.WriteLine("             2- Realizar um saque:        ");
            Console.WriteLine("             =============================");
            Console.WriteLine("             3- Consultar Saldo:          ");
            Console.WriteLine("             =============================");
            Console.WriteLine("             4- Extrato:                  ");
            Console.WriteLine("             =============================");
            Console.WriteLine("             5- Voltar ao menu Principal: ");
            Console.WriteLine("             =============================");

            opcao= int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:
                    Deposito(pessoa);
                    break;
                case 2:
                    Saque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("         Opcao Invalida! Tente novamente          ");
                    break;
            }
        }

       private static void Deposito(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);
            Console.WriteLine("         Digite o valor do deposito:          ");
            double valor= Double.Parse(Console.ReadLine());
            Console.WriteLine("         ==================================== ");
            pessoa.Conta.Deposita(valor);
            Console.Clear();
            TelaBoasVindas(pessoa);

            Console.WriteLine("                                              ");
            Console.WriteLine("                                              ");
            Console.WriteLine("         Deposito Realizado com sucesso       ");
            Console.WriteLine("                                              ");
            Console.WriteLine("                                              ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void Saque(Pessoa pessoa)
        {
            Console.Clear();
            TelaBoasVindas(pessoa);
            Console.WriteLine("         Digite o valor do saque:             ");
            double valor = Double.Parse(Console.ReadLine());
            Console.WriteLine("         ==================================== ");
            bool saque= pessoa.Conta.Saca(valor);
            Console.Clear();
            TelaBoasVindas(pessoa);

            if (saque)
            {
                Console.WriteLine("                                              ");
                Console.WriteLine("                                              ");
                Console.WriteLine("         Saque Realizado com sucesso          ");
                Console.WriteLine("                                              ");
                Console.WriteLine("                                              ");
            //    OpcaoVoltarLogado(pessoa);
            }
            else
            {
                Console.WriteLine("                                              ");
                Console.WriteLine("                                              ");
                Console.WriteLine("         Saldo insuficiente!                  ");
                Console.WriteLine("                                              ");
                Console.WriteLine("                                              ");
            }
            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);
            Console.WriteLine($"         Seu saldo e: {pessoa.Conta.ConsultaSaldo()}       ");
            Console.WriteLine("         ====================================               ");
            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            if (pessoa.Conta.Extratoo().Any())
            {
                //Mostrar extrato
                double total = pessoa.Conta.Extratoo().Sum(x => x.Valor);
                foreach (Extrato extrato in pessoa.Conta.Extratoo())
                {
                    Console.WriteLine("                                                            ");
                    Console.WriteLine($"       DATA: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($"       Tipo de Movimentacao: {extrato.Descricao}           ");
                    Console.WriteLine($"       Valor: {extrato.Valor}                              ");
                    Console.WriteLine("        ====================================================");
                }


                Console.WriteLine($"                                                           ");
                    Console.WriteLine("                                                            ");
                    Console.WriteLine($"          SUB TOTAL: {total}                               ");
                    Console.WriteLine("         ====================================               ");

                }
            else
                {
                    Console.WriteLine($"         Nao ha extrato a ser exibido                      ");
                    Console.WriteLine("         ====================================               ");
                }

                OpcaoVoltarLogado(pessoa);
        }

        private static void VoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("         Entre com uma opcao abaixo:          ");
            Console.WriteLine("         ===========================          ");
            Console.WriteLine("         1- Voltar para a minha conta         ");
            Console.WriteLine("         ===========================          ");
            Console.WriteLine("         2- Sair                              ");
            Console.WriteLine("         ===========================          ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao==1)
            {
                TelaContaLogada(pessoa);
            }
            else
            {
                TelaPrincipal();
            }
        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("         Entre com uma opcao abaixo:             ");
            Console.WriteLine("         ===========================             ");
            Console.WriteLine("         1- Voltar para o menu principal         ");
            Console.WriteLine("         ===========================             ");
            Console.WriteLine("         2- Sair                                 ");
            Console.WriteLine("         ===========================             ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                TelaContaLogada(pessoa);
            }
            else
            {
                Console.WriteLine("         Opcao Invalida!                         ");
                Console.WriteLine("         ===========================             ");
            }
        }
    }
}
