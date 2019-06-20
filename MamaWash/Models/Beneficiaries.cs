using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MamaWash.Models
{
    public class Beneficiaries
    {
        public int ID { get; set; }
        [StringLength(10, MinimumLength = 10)]
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }

        [Display(Name ="Bank")]
        public string BankID { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string RecipientCode { get; set; }

        public ICollection<TransactionHistory> Transactions { get; set; }
    }
}
