using COPaymentGateWay.WebApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Validators
{
    public class GetPaymentDetailsRequestValidator : AbstractValidator<GetPaymentDetailsRequest>
    {
        public GetPaymentDetailsRequestValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.PaymentIdentifier).NotEqual(Guid.Empty).WithMessage("Invalid Identifier");
        }
    }
}
