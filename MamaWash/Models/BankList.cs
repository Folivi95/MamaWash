using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamaWash.Models
{
    public class BankList
    {
        public int ID { get; set; }
        public string BankCode { get; set; }
        public string Bank { get; set; }
    }
}
