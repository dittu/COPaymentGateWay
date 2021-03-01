using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Extensions
{
    public static class CardNumberHelper
    {
        public static string MaskCardNumber(this string cardNumber)
        {
            var replaceSpacesAndHypens = cardNumber.Replace(" ", "").Replace("-", "");

            var cardNumberLength = replaceSpacesAndHypens.Length;

            return $"{replaceSpacesAndHypens.Substring(0, 4)}{new string('X', (cardNumberLength - 8))}{replaceSpacesAndHypens.Substring(cardNumberLength - 4, 4)}";
        }
    }
}
