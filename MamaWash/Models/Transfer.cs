using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MamaWash.Models
{
    public class Transfer
    {
        [Display(Name ="S/N")]
        public int ID { get; set; }
        public int PIN { get; set; }
        public string Source { get; set; }
        public string Reason { get; set; }
        [Required]
        public string Recipient { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Name { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name ="Bank Name")]
        public string BankName { get; set; }
        public string Status { get; set; }

    }
}
