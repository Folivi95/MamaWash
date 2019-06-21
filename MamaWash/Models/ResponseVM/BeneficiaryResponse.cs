using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MamaWash.Models
{
    public class BeneficiaryResponse
    {
        public bool status { get; set; }
        public List<Beneficiaries> data { get; set; }
    }

    public class Beneficiaries
    {
        public string name { get; set; }
        public string recipient_code { get; set; }
        public BeneficiaryDetails details { get; set; }
    }

    public class BeneficiaryDetails
    {
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string bank_code { get; set; }
    }
}
