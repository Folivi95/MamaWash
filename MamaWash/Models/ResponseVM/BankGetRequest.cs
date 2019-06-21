using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MamaWash.Models
{
    public class BankGetRequest
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<BankItem> data { get; set; }
    }

    public class BankItem
    {
        public string code { get; set; }
        public string name { get; set; }

    }

    public class BalanceData
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<Balance> data { get; set; }
    }

    public class Balance
    {
        public string balance { get; set; }
    }
}
