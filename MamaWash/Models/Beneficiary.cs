using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MamaWash.Models
{
    public class Beneficiary
    {
        public int ID { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage ="Account Number must be 10 digits"), Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name ="Account Name")]
        public string AccountName { get; set; }
        public BankList Bank { get; set; }
        public string RecipientCode { get; set; }
    }
}
