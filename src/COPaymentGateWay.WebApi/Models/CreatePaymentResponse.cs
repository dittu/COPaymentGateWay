using COPaymentGateWay.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Models
{
    public class CreatePaymentResponse
    {
        public string Identifier { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
