using DigiBank.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public abstract class Conta : Banco, IConta
    {

        public Conta() {
            this.NumeroAgencia = "0001";
            Conta.NumeroDaContaSequencial++;
            this.Movimentacoes = new List<Extrato>();
        }

        public double Saldo { get; protected set; }
        public string NumeroAgencia { get; private set; }
        public string NumeroConta { get; protected set; }

        public static int NumeroDaContaSequencial { get; private set; }

        private List<Extrato> Movimentacoes;
        public double ConsultaSaldo()
        {
            return this.Saldo;
        }

        public bool Saca(double valor)
        {
            if(valor>this.ConsultaSaldo())
            {
                return false;
            }
            DateTime dataactual = DateTime.Now;
            this.Movimentacoes.Add(new Extrato(dataactual, "Levantamento", -valor));
            this.Saldo -= valor;
            return true;
        }
        public void Deposita(double valor)
        {
            DateTime dataactual = DateTime.Now;
            this.Movimentacoes.Add(new Extrato(dataactual, "Deposito", valor));
            this.Saldo += valor;
        }

        public string GetCodigoDoBanco()
        {
            return this.CodigoDoBanco;
        }

        public string GetNumeroDaConta()
        {
            return this.NumeroConta;
        }

        public string GetNumeroAgencia()
        {
            return this.NumeroAgencia;
        }

        public List<Extrato> Extratoo()
        {
            return this.Movimentacoes;
        }
    }
}
