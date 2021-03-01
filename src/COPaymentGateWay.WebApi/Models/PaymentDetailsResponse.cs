using COPaymentGateWay.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Models
{
    public class PaymentDetailsResponse
    {
        public string Identifier { get; set; }

        public CardDetails CardDetails { get; set; }

        public decimal Amount { get; set; }

        public CurrencyCode Currency { get; set; }

        public string Reference { get; set; }

    }
}
