using COPaymentGateWay.Core.Interfaces;
using COPaymentGateWay.Core.MockBank.Interfaces;
using COPaymentGateWay.Core.Models;
using COPaymentGateWay.WebApi.Dtos;
using COPaymentGateWay.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace COPaymentGateWay.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private IPaymentsRepository _paymentRepo;
        private ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentsRepo"></param>
        /// <param name="logger"></param>
        public PaymentsController(IPaymentsRepository paymentsRepo,
                                  ILogger<PaymentsController> logger)
        {
            _paymentRepo = paymentsRepo ?? throw new ArgumentNullException(nameof(paymentsRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createPayment")]
        [ProducesResponseType(typeof(CreatePaymentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest paymentRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            string merchantId = identity.FindFirst("MerchantId").Value;

            var paymentEntry = paymentRequest.ConvertToPaymentEntry();

            paymentEntry.MerchantId = merchantId;

            var processPaymentRes = await _paymentRepo.ProcessPayment(paymentEntry);

            if (processPaymentRes.Success)
                return CreatedAtAction(nameof(CreatePayment), new CreatePaymentResponse()
                {
                    Identifier = paymentEntry.Identifier.ToString(),
                    Status = paymentEntry.Status
                });
            else
                return StatusCode(500);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Route("getPaymentDetails")]
        [ProducesResponseType(typeof(PaymentDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentDetails([FromBody] GetPaymentDetailsRequest paymentDetailsRequest)
        {

            _logger.LogInformation($"GetPaymentDetails requested");

            var res = await _paymentRepo.GetPaymentEntry(paymentDetailsRequest.PaymentIdentifier);

            if (res.Success)
            {
                if (res.PaymentEntry == null)
                    return NotFound();
                else
                    return Ok(res.PaymentEntry.ConvertToPaymentDetails());
            }
            else
                return StatusCode(500);
        }

    }
}
