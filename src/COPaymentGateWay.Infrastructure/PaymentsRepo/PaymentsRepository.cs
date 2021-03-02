using COPaymentGateWay.Core.Interfaces;
using COPaymentGateWay.Core.MockBank.Interfaces;
using COPaymentGateWay.Core.Models;
using COPaymentGateWay.Infrastructure.PaymentsRepo.Convertors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPaymentGateWay.Infrastructure.PaymentsRepo
{


    public class PaymentsRepository : IPaymentsRepository

    {
        private IPaymentsDataAccess _paymentsDataAcces;
        private IMockBankRepository _mockBankRepo;
        private ILogger _logger;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_paymentsDataAccess"></param>
        /// <param name="mockBankRepo"></param>
        /// <param name="logger"></param>
        public PaymentsRepository(IPaymentsDataAccess _paymentsDataAccess,
                                  IMockBankRepository mockBankRepo,
                                  ILogger<PaymentsRepository> logger)
        {
            _paymentsDataAcces = _paymentsDataAccess ?? throw new ArgumentNullException(nameof(_paymentsDataAccess));
            _mockBankRepo = mockBankRepo ?? throw new ArgumentNullException(nameof(mockBankRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<GetPaymentEntryResult> GetPaymentEntry(Guid paymentIdentifier)
        {
            return await _paymentsDataAcces.GetPaymentEntry(paymentIdentifier);
        }

        public async Task<BaseResult> ProcessPayment(PaymentEntry paymentEntry)
        {
            BaseResult res = new BaseResult();

           var initialDataAccessRes =  await _paymentsDataAcces.AddPayment(paymentEntry);

            var bankRequest = paymentEntry.ConvertToMockPaymentRequest();

            var requestPaymentResponse = _mockBankRepo.RequestPayment(bankRequest);

            paymentEntry.Status = requestPaymentResponse.Status;
            paymentEntry.BankIdentifier = requestPaymentResponse.Identifier;
            paymentEntry.BankStatus = requestPaymentResponse.Status;

            //TODO: Replace with partial update
            var updateRes = await _paymentsDataAcces.AddPayment(paymentEntry);


            if (initialDataAccessRes.Success && 
                requestPaymentResponse.HttpStatus == System.Net.HttpStatusCode.OK && 
                updateRes.Success)
            {
                res.Success = true;
            }
            else
                res.Message = $"Error processing payments."; //might be a throw

            return res;
        }
    }
}
