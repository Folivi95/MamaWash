﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MamaWash.Models
{
    public class CreateRecipient
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Recipient data { get; set; }
    }

    public class Recipient
    {
        public string recipient_code { get; set; }
    }

}
