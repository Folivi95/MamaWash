using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MamaWash.Models
{
    public class Beneficiaries
    {
        public int ID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string BankID { get; set; }
        public BankList BankName { get; set; }
        public string RecipientCode { get; set; }

        public ICollection<TransactionHistory> Transactions { get; set; }
    }
}
