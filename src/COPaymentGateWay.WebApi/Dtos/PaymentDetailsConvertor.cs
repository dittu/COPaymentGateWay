using COPaymentGateWay.Core.Models;
using COPaymentGateWay.WebApi.Extensions;
using COPaymentGateWay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Dtos
{
    public static class PaymentDetailsConvertor
    {
        public static PaymentDetailsResponse ConvertToPaymentDetails(this PaymentEntry entry)
        {
            return new PaymentDetailsResponse()
            {
                Amount = entry.Amount,
                Currency = entry.Currency,
                Identifier = entry.Identifier.ToString(),
                Reference = entry.RefText,
                CardDetails = new CardDetails()
                {
                    CardNumber = entry.CardNumber.MaskCardNumber(),
                    Cvv = entry.Cvv,
                    ExpiryMonth = entry.ExpiryMonth,
                    ExpiryYear = entry.ExpiryYear,
                    HolderName = entry.HolderName
                }

            };

        }
    }
}
