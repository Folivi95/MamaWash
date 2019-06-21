using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MamaWash.Models.ResponseVM
{
    public class TransferResponseVM
    {
        public bool status { get; set; }
        public TransferData data { get; set; }
    }

    public class TransferData
    {
        public string status { get; set; }
        public string transfer_code { get; set; }

    }
}
