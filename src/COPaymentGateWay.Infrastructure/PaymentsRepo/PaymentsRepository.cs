using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using COPaymentGateWay.Core.Interfaces;
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
        public IAmazonDynamoDB _dynamoDB;
        public ITableConfig _paymentConfig;
        public ILogger _logger;

        public PaymentsRepository(IAmazonDynamoDB dynamoDB, ITableConfig paymentConfig, ILogger<PaymentsRepository> logger)
        {
            _dynamoDB = dynamoDB;
            _paymentConfig = paymentConfig;
            _logger = logger;
        }

        public async Task<BaseResult> AddPayment(PaymentEntry paymentEntry)
        {
            _logger.LogDebug($"Initiating AddPayment Operation");

            BaseResult res = new BaseResult();

            PutItemRequest putRequest = new PutItemRequest
            {
                TableName = _paymentConfig.TableName,
                Item = paymentEntry.ConvertToDynamoDocument().ToAttributeMap()
            };


            var response = await _dynamoDB.PutItemAsync(putRequest);

            //TODO: When usage increases make a common extension method to determine success of dynamoresponse consumer
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                res.Success = true;
            else
                res.Success = false;

            res.Message = $"DynamoDB Response HttpStatusCode: {response.HttpStatusCode}";

            _logger.LogDebug($"{res.Message}");

            return res;

        }

        public async Task<GetPaymentEntryResult> GetPaymentEntry(Guid paymentIdentifier)
        {
            _logger.LogDebug($"Initiating QueryPayment Operation");

            GetPaymentEntryResult res = new GetPaymentEntryResult();

            QueryRequest request = new QueryRequest
            {
                TableName = _paymentConfig.TableName,
                KeyConditions = new Dictionary<string, Condition> {
                    {
                      _paymentConfig.HashKey.ColumnName,
                        new Condition {
                            ComparisonOperator = ComparisonOperator.EQ,
                            AttributeValueList = new List<AttributeValue> { new AttributeValue { S = paymentIdentifier.ToString() } }
                        }

                    }

                },
            };

            var queryResponse = await _dynamoDB.QueryAsync(request);

            if (queryResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                res.Success = true;
            if (queryResponse.Items.Count > 0)
                res.PaymentEntry = PaymentEntryExtensions.ConvertToPaymentEntry(queryResponse.Items.First());

            res.Message = $"DynamoDB Response HttpStatusCode: {queryResponse.HttpStatusCode}. ItemCount: {queryResponse.Items.Count}";

            _logger.LogDebug($"{res.Message}");


            return res;
        }
    }
}
