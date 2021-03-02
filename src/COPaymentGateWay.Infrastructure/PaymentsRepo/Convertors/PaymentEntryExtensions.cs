using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using COPaymentGateWay.Core.Enum;
using COPaymentGateWay.Core.MockBank.Models;
using COPaymentGateWay.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace COPaymentGateWay.Infrastructure.PaymentsRepo.Convertors
{
    public static class PaymentEntryExtensions
    {
        public static Document ConvertToDynamoDocument(this PaymentEntry entry)
        {
            Document doc = new Document();
            doc[nameof(entry.Identifier)] = entry.Identifier;
            doc[nameof(entry.MerchantId)] = entry.MerchantId;
            doc[nameof(entry.RequestDateTime)] = entry.RequestDateTime;
            doc[nameof(entry.HolderName)] = entry.HolderName;
            doc[nameof(entry.CardNumber)] = entry.CardNumber;
            doc[nameof(entry.ExpiryMonth)] = entry.ExpiryMonth;
            doc[nameof(entry.ExpiryYear)] = entry.ExpiryYear;
            doc[nameof(entry.Cvv)] = entry.Cvv;
            doc[nameof(entry.Currency)] = (int)entry.Currency;
            doc[nameof(entry.Amount)] = entry.Amount;
            doc[nameof(entry.Status)] = (int)entry.Status;
            doc[nameof(entry.RefText)] = entry.RefText;
            doc[nameof(entry.BankIdentifier)] = entry.BankIdentifier;
            doc[nameof(entry.BankStatus)] = (int)entry.BankStatus;
            return doc;
        }

        public static PaymentEntry ConvertToPaymentEntry(Dictionary<string, AttributeValue> attributes)
        {
            PaymentEntry entry = new PaymentEntry();
            entry.Identifier = Guid.Parse(attributes[nameof(entry.Identifier)].S);
            entry.MerchantId = attributes[nameof(entry.MerchantId)].S;
            entry.RequestDateTime = DateTime.Parse(attributes[nameof(entry.RequestDateTime)].S);
            entry.HolderName = attributes[nameof(entry.HolderName)].S;
            entry.CardNumber = attributes[nameof(entry.CardNumber)].S;
            entry.ExpiryMonth = int.Parse(attributes[nameof(entry.ExpiryMonth)].N);
            entry.ExpiryYear = int.Parse(attributes[nameof(entry.ExpiryYear)].N);
            entry.Cvv = attributes[nameof(entry.Cvv)].S;
            entry.Currency = (CurrencyCode)Enum.Parse(typeof(CurrencyCode), attributes[nameof(entry.Currency)].N);
            entry.Amount = Decimal.Parse(attributes[nameof(entry.Amount)].N);
            entry.Status = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), attributes[nameof(entry.Status)].N);
            entry.RefText = attributes[nameof(entry.RefText)].S;
            entry.BankIdentifier = attributes[nameof(entry.BankIdentifier)].S;
            entry.BankStatus = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), attributes[nameof(entry.BankStatus)].N);
            return entry;

        }

        public static MockBankPaymentRequest ConvertToMockPaymentRequest(this PaymentEntry paymentEntry)
        {
            return new MockBankPaymentRequest()
            {
                Amount = paymentEntry.Amount,
                CardNumber = paymentEntry.CardNumber,
                HolderName = paymentEntry.HolderName,
                Cvv = paymentEntry.Cvv,
                ExpiryMonth = paymentEntry.ExpiryMonth,
                ExpiryYear = paymentEntry.ExpiryYear,
                CurrencyCode = paymentEntry.ToString()
            };
        }

    }
}
