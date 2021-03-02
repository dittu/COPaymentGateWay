using COPaymentGateWay.Core.Interfaces;
using COPaymentGateWay.Core.MockBank.Interfaces;
using COPaymentGateWay.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using COPaymentGateWay.Core.Models;
using COPaymentGateWay.WebApi.Models;
using UnitTests.Fakes;
using COPaymentGateWay.Core.MockBank.Models;
using COPaymentGateWay.Core.Enum;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class PaymentsControllerTests
    {

        public PaymentsController Controller { get; set; }

        public Mock<IPaymentsRepository> MockPaymentsRepository { get; set; }

        public Mock<ILogger<PaymentsController>> MockLogger { get; set; }

        [SetUp]
        public void Setup()
        {
            this.MockPaymentsRepository = new Mock<IPaymentsRepository>();
            this.MockLogger = new Mock<ILogger<PaymentsController>>();
            this.Controller = new PaymentsController(MockPaymentsRepository.Object, MockLogger.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                            {
                                            new Claim("MerchantId", "MockMerchantId"),
                            }, "mock"));

            this.Controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Test]
        public async Task GetPaymentsWithOutAnyExistingShouldHaveResponse()
        {
            MockPaymentsRepository.Setup(x => x.GetPaymentEntry(It.IsAny<Guid>())).ReturnsAsync(new GetPaymentEntryResult() { Success = true, PaymentEntry = null });
            var response = await this.Controller.GetPaymentDetails(new GetPaymentDetailsRequest() { PaymentIdentifier = Guid.NewGuid() }) as NotFoundResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [Test]
        public async Task GetPaymentsWithExistingPaymentsShouldReturnData()
        {
            MockPaymentsRepository.Setup(x => x.GetPaymentEntry(It.IsAny<Guid>())).ReturnsAsync(new GetPaymentEntryResult() { Success = true, PaymentEntry = FakePaymentData.FakePaymentEntryData() });
            var response = await this.Controller.GetPaymentDetails(new GetPaymentDetailsRequest() { PaymentIdentifier = Guid.NewGuid() }) as OkObjectResult;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Value);
        }

        [Test]
        public async Task CreatePaymentRequestShouldReturn201Response()
        {

            MockPaymentsRepository.Setup(x => x.ProcessPayment(It.IsAny<PaymentEntry>())).ReturnsAsync(new BaseResult() { Success = true, Message = "example message" });

            
            var response = await this.Controller.CreatePayment(FakeCreatePaymentData.FakeCreatePaymentRequest()) as CreatedAtActionResult;


            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Value);
            Assert.AreEqual(201, response.StatusCode);
        }


    }
}
