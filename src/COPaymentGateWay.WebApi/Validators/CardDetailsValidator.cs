using COPaymentGateWay.WebApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Validators
{
    public class CardDetailsValidator : AbstractValidator<CardDetails>
    {
        public CardDetailsValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Cvv).NotEmpty().Matches("[0-9]{3}$").WithMessage("Invalid Card Details");
            RuleFor(x => x.ExpiryMonth)
                   .InclusiveBetween(1, 12)
                   .DependentRules(() =>
                                        RuleFor(x => x.ExpiryYear)
                                        .InclusiveBetween(2021, 2099)
                                        .Must((x, expiryYear) => new DateTime(expiryYear, x.ExpiryMonth, DateTime.DaysInMonth(expiryYear, x.ExpiryMonth), 23, 59, 59) > DateTime.UtcNow))
                                        .WithMessage("Invalid Card Details");

            RuleFor(x => x.HolderName).NotEmpty().WithMessage("Invalid Card Details");
            RuleFor(x => x.CardNumber)
                .Must(x => CreditCardValidator.CreditCardStringExtension.ValidCreditCardBrand(x,
                                                                                             new CreditCardValidator.CardIssuer[] { CreditCardValidator.CardIssuer.Visa,
                                                                                                 CreditCardValidator.CardIssuer.MasterCard}))
                .Must(x => CreditCardValidator.Luhn.CheckLuhn(x)).WithMessage("Invalid Card Details");
        }
    }
}
