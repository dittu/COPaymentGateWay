using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Models
{
    public class CardDetails
    {
        public string HolderName { get; set; }
        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string Cvv { get; set; }
    }
}
