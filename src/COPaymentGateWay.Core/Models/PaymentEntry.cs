using COPaymentGateWay.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPaymentGateWay.Core.Models
{
    public class PaymentEntry
    {
        public Guid Identifier { get; set; }

        public string MerchantId { get; set; }
        public DateTime RequestDateTime { get; set; }

        public string HolderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public CurrencyCode Currency { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string RefText { get; set; }

        public string BankIdentifier { get; set; }

        public PaymentStatus BankStatus { get; set; }
    }
}
