using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MamaWash.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public Beneficiary RecipientCode { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }

    }
}
