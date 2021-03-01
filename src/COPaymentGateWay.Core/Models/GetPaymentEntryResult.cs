using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPaymentGateWay.Core.Models
{
    public class GetPaymentEntryResult : BaseResult
    {
        public PaymentEntry PaymentEntry { get; set; }
    }
}
