using COPaymentGateWay.WebApi.Models;
using COPaymentGateWay.WebApi.Validators;
using NUnit.Framework;

namespace UnitTests.ValidatiorTests
{
    [TestFixture]
    public class CardDetailsValidatorTests
    {
        public CardDetailsValidator CardDetailsValidator { get; set; }

        [SetUp]
        public void Setup()
        {
            CardDetailsValidator = new CardDetailsValidator();
        }

        [Test]
        public void Valid_Visa_Card_ShouldBe_Valid()
        {
            CardDetails visaCard = new CardDetails()
            {
                HolderName = "Visa Card",
                ExpiryMonth = 12,
                ExpiryYear = 2021,
                CardNumber = "4613976229878",
                Cvv = "456"
            };

            var res = CardDetailsValidator.Validate(visaCard);

            Assert.IsTrue(res.IsValid);
        }


        [Test]
        public void Valid_Master_Card_ShouldBe_Valid()
        {
            CardDetails masterCard = new CardDetails()
            {
                HolderName = "Master Card",
                ExpiryMonth = 12,
                ExpiryYear = 2021,
                CardNumber = "2486977978816328",
                Cvv = "456"
            };

            var res = CardDetailsValidator.Validate(masterCard);

            Assert.IsTrue(res.IsValid);
        }

        [Test]
        public void Expired_Date_Card_ShouldFail_Validation()
        {
            CardDetails visaCard = new CardDetails()
            {
                HolderName = "Visa Card",
                ExpiryMonth = 1,
                ExpiryYear = 2021,
                CardNumber = "2486977978816328",
                Cvv = "456"
            };

            var res = CardDetailsValidator.Validate(visaCard);

            Assert.IsFalse(res.IsValid);
        }


        [Test]
        public void Expired_Year_Card_ShouldFail_Validation()
        {
            CardDetails visaCard = new CardDetails()
            {
                HolderName = "Visa Card",
                ExpiryMonth = 10,
                ExpiryYear = 2020,
                CardNumber = "2486977978816328",
                Cvv = "456"
            };

            var res = CardDetailsValidator.Validate(visaCard);

            Assert.IsFalse(res.IsValid);
        }

        //571072310402
        [Test]
        public void Masetro_Card_ShouldFail_Validation()
        {
            CardDetails maestroCard = new CardDetails()
            {
                HolderName = "Maestro Card",
                ExpiryMonth = 12,
                ExpiryYear = 2021,
                CardNumber = "571072310402",
                Cvv = "456"
            };

            var res = CardDetailsValidator.Validate(maestroCard);

            Assert.IsFalse(res.IsValid);
        }

        [Test]
        public void Random_Card_Number_Should_Fail_Lunhs_Validation()
        {
            CardDetails visaCard = new CardDetails()
            {
                HolderName = "Visa Card",
                ExpiryMonth = 12,
                ExpiryYear = 2021,
                CardNumber = "2486977978316328",
                Cvv = "456"
            };

            var res = CardDetailsValidator.Validate(visaCard);

            Assert.IsFalse(res.IsValid);
        }

    }
}
