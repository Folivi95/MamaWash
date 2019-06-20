using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamaWash.Models
{
    public class AccountValidation
    {
        [Required, StringLength(10)]
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }

    }
}
