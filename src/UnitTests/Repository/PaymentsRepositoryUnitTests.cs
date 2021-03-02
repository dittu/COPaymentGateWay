using COPaymentGateWay.Core.Interfaces;
using COPaymentGateWay.Core.MockBank.Interfaces;
using COPaymentGateWay.Core.MockBank.Models;
using COPaymentGateWay.Core.Models;
using COPaymentGateWay.Infrastructure.PaymentsRepo;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Repository
{
    public class PaymentsRepositoryUnitTests
    {


        public Mock<IPaymentsDataAccess> MockPaymentsDataAccess { get; set; }

        public Mock<IMockBankRepository> MockBankRepository { get; set; }

        public Mock<ILogger<PaymentsRepository>> MockLogger { get; set; }

        public IPaymentsRepository PaymentsRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            this.MockBankRepository = new Mock<IMockBankRepository>();
            this.MockPaymentsDataAccess = new Mock<IPaymentsDataAccess>();
            this.MockLogger = new Mock<ILogger<PaymentsRepository>>();
            this.PaymentsRepository = new PaymentsRepository(MockPaymentsDataAccess.Object,
                                                             MockBankRepository.Object,
                                                             MockLogger.Object);
        }

        [Test]
        public void ProcessPaymentShouldBeASucessWhenBankRepoAndDataAccessAreASuccess()
        {
            MockBankRepository.Setup(x => x.RequestPayment(It.IsAny<MockBankPaymentRequest>()))
                              .Returns(new MockBankResponse() 
                              { 
                                  HttpStatus = System.Net.HttpStatusCode.OK, 
                                  Identifier = "Dummy", 
                                  Status = COPaymentGateWay.Core.Enum.PaymentStatus.Authorized            
                              });

            MockPaymentsDataAccess.Setup(x => x.AddPayment(It.IsAny<PaymentEntry>()))
                                  .ReturnsAsync(new BaseResult() { Success = true });

            var res = this.PaymentsRepository.ProcessPayment(Fakes.FakePaymentData.FakePaymentEntryData());

            Assert.IsTrue(res.IsCompletedSuccessfully);
            Assert.IsTrue(res.Result.Success);

            MockBankRepository.Verify(x => x.RequestPayment(It.IsAny<MockBankPaymentRequest>()), Times.Once);
            MockPaymentsDataAccess.Verify(x => x.AddPayment(It.IsAny<PaymentEntry>()), Times.Exactly(2));
        }

        [Test]
        public void ProcessPaymentShouldBeAFailWhenBankCallFails()
        {
            MockBankRepository.Setup(x => x.RequestPayment(It.IsAny<MockBankPaymentRequest>()))
                              .Returns(new MockBankResponse()
                              {
                                  HttpStatus = System.Net.HttpStatusCode.InternalServerError,
                                  Identifier = "Dummy",
                                  Status = COPaymentGateWay.Core.Enum.PaymentStatus.Authorized
                              });

            MockPaymentsDataAccess.Setup(x => x.AddPayment(It.IsAny<PaymentEntry>()))
                                  .ReturnsAsync(new BaseResult() { Success = true });

            var res = this.PaymentsRepository.ProcessPayment(Fakes.FakePaymentData.FakePaymentEntryData());

            Assert.IsTrue(res.IsCompletedSuccessfully);
            Assert.IsFalse(res.Result.Success);

            MockBankRepository.Verify(x => x.RequestPayment(It.IsAny<MockBankPaymentRequest>()), Times.Once);
            MockPaymentsDataAccess.Verify(x => x.AddPayment(It.IsAny<PaymentEntry>()), Times.Exactly(2));
        }

        [Test]
        public void ProcessPaymentShouldBeAFailWhenDAtaAccessCallFails()
        {
            MockBankRepository.Setup(x => x.RequestPayment(It.IsAny<MockBankPaymentRequest>()))
                              .Returns(new MockBankResponse()
                              {
                                  HttpStatus = System.Net.HttpStatusCode.OK,
                                  Identifier = "Dummy",
                                  Status = COPaymentGateWay.Core.Enum.PaymentStatus.Authorized
                              });

            MockPaymentsDataAccess.Setup(x => x.AddPayment(It.IsAny<PaymentEntry>()))
                                  .ReturnsAsync(new BaseResult() { Success = false });

            var res = this.PaymentsRepository.ProcessPayment(Fakes.FakePaymentData.FakePaymentEntryData());

            Assert.IsTrue(res.IsCompletedSuccessfully);
            Assert.IsFalse(res.Result.Success);

            MockBankRepository.Verify(x => x.RequestPayment(It.IsAny<MockBankPaymentRequest>()), Times.Once);
            MockPaymentsDataAccess.Verify(x => x.AddPayment(It.IsAny<PaymentEntry>()), Times.Exactly(2));
        }

        [Test]
        public void GetPaymentEntryShouldCallDataAccessOnce()
        {
            
            MockPaymentsDataAccess.Setup(x => x.GetPaymentEntry(It.IsAny<Guid>()))
                                  .ReturnsAsync(new GetPaymentEntryResult() { Success = true, PaymentEntry = Fakes.FakePaymentData.FakePaymentEntryData()});

            var res = this.PaymentsRepository.GetPaymentEntry(Guid.NewGuid());

            Assert.IsTrue(res.IsCompletedSuccessfully);
            Assert.IsTrue(res.Result.Success);

            MockPaymentsDataAccess.Verify(x => x.GetPaymentEntry(It.IsAny<Guid>()), Times.Exactly(1));
        }
    }
}
