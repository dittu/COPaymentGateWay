using COPaymentGateWay.Core.Enum;
using COPaymentGateWay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Fakes
{
    public class FakeCreatePaymentData
    {
        public static CreatePaymentRequest FakeCreatePaymentRequest()
        {
            return new CreatePaymentRequest()
            {
                Amount = 100m,
                CardDetails = FakeCardData.FakeValidVisaCard(),
                Currency = CurrencyCode.EUR,
                Reference = "Sample"
            };
        }
    }
}
