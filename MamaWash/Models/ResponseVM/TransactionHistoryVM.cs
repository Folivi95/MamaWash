using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MamaWash.Models.ResponseVM
{
    public class TransactionHistoryVM
    {
        public bool status { get; set; }
        public List<Transactions> data { get; set; }
    }

    public class Transactions
    {
        public decimal amount { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public Recipient recipient { get; set; }
    }

    public class Recipient
    {
        public string name { get; set; }
        public Details details { get; set; }
    }

    public class Details
    {
        public string account_number { get; set; }
        public string bank_name { get; set; }
    }
}
