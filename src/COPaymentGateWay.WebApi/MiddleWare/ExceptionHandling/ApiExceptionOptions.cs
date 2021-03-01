﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace COPaymentGateWay.WebApi.MiddleWare.ExceptionHandling
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
        public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
    }
}
