using COPaymentGateWay.Core.MockBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPaymentGateWay.Core.MockBank.Interfaces
{
    public interface IMockBankRepository
    {
        public MockBankResponse RequestPayment(MockBankPaymentRequest request);
    }
}
