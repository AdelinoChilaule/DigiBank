using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Extrato
    {
        public Extrato(DateTime Data, string Descricao, double Valor)
        {
            this.Data= Data;
            this.Descricao= Descricao;
            this.Valor= Valor;

        }
 

        public DateTime Data { get; private set; }

        public string Descricao { get; private set; }

        public double Valor { get; private set; }
    }
}
