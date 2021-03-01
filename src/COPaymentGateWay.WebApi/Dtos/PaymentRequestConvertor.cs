using COPaymentGateWay.Core.MockBank.Models;
using COPaymentGateWay.Core.Models;
using COPaymentGateWay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Dtos
{
    public static class PaymentRequestConvertor
    {
        public static MockBankPaymentRequest ConvertToMockPaymentRequest(this CreatePaymentRequest paymentRequest)
        {
            return new MockBankPaymentRequest()
            {
                Amount = paymentRequest.Amount,
                CardNumber = paymentRequest.CardDetails.CardNumber,
                HolderName = paymentRequest.CardDetails.HolderName,
                Cvv = paymentRequest.CardDetails.Cvv,
                ExpiryMonth = paymentRequest.CardDetails.ExpiryMonth,
                ExpiryYear = paymentRequest.CardDetails.ExpiryYear,
                CurrencyCode = paymentRequest.Currency.ToString()
            };
        }

        public static PaymentEntry ConvertToPaymentEntry(this CreatePaymentRequest paymentRequest)
        {
            return new PaymentEntry()
            {
                Amount = paymentRequest.Amount,
                CardNumber = paymentRequest.CardDetails.CardNumber,
                HolderName = paymentRequest.CardDetails.HolderName,
                Cvv = paymentRequest.CardDetails.Cvv,
                ExpiryMonth = paymentRequest.CardDetails.ExpiryMonth,
                ExpiryYear = paymentRequest.CardDetails.ExpiryYear,
                Currency = paymentRequest.Currency,
                RefText = paymentRequest.Reference,
                MerchantId = "TODO",
                Identifier = Guid.NewGuid(),
                RequestDateTime = DateTime.UtcNow
            };
        }

    }
}
