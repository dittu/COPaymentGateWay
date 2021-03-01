using COPaymentGateWay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPaymentGateWay.Core.Interfaces
{
    public interface IPaymentsRepository
    {
        public Task<BaseResult> AddPayment(PaymentEntry paymentEntry);

        public Task<GetPaymentEntryResult> GetPaymentEntry(Guid paymentIdentifier);
    }
}
