using COPaymentGateWay.Core.MockBank.Interfaces;
using COPaymentGateWay.Core.MockBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace COPaymentGateWay.Infrastructure.MockBankRepo
{
    public class MockBankRepository : IMockBankRepository
    {
        public MockBankResponse RequestPayment(MockBankPaymentRequest request)
        {
            MockBankResponse resp = new MockBankResponse();

            switch (request.Amount)
            {
                case decimal n when (n > 0 && n <= 100):
                    resp.HttpStatus = System.Net.HttpStatusCode.OK;
                    resp.Status = Core.Enum.PaymentStatus.Authorized;
                    resp.Identifier = Guid.NewGuid().ToString();
                    break;

                case decimal n when (n > 100 && n <= 200):
                    resp.HttpStatus = System.Net.HttpStatusCode.OK;
                    resp.Status = Core.Enum.PaymentStatus.CardVerified;
                    resp.Identifier = Guid.NewGuid().ToString();
                    break;

                case decimal n when (n > 200 && n <= 300):
                    resp.HttpStatus = System.Net.HttpStatusCode.OK;
                    resp.Status = Core.Enum.PaymentStatus.Declined;
                    resp.Identifier = Guid.NewGuid().ToString();
                    break;

                case decimal n when (n > 300 && n <= 400):
                    resp.HttpStatus = System.Net.HttpStatusCode.OK;
                    resp.Status = Core.Enum.PaymentStatus.Paid;
                    resp.Identifier = Guid.NewGuid().ToString();
                    break;

                case decimal n when (n > 400 && n <= 500):
                    resp.HttpStatus = System.Net.HttpStatusCode.InternalServerError;
                    resp.Status = Core.Enum.PaymentStatus.Unknown;
                    resp.Identifier = Guid.NewGuid().ToString();
                    break;

                default:
                    resp.HttpStatus = System.Net.HttpStatusCode.OK;
                    resp.Status = Core.Enum.PaymentStatus.Paid;
                    resp.Identifier = Guid.NewGuid().ToString();
                    break;
            }

            return resp;
        }
    }
}
