using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPaymentGateWay.Core.Enum
{
    public enum PaymentStatus
    {
        Unknown = 0,
        Pending = 1,
        Authorized = 2,
        CardVerified = 3,
        Voided = 4,
        PartiallyCaptured = 5,
        Captured = 6,
        PartiallyRefunded = 7,
        Refunded = 8,
        Declined = 9,
        Cancelled = 10,
        Paid = 11
    }
}
