using Amazon.DynamoDBv2;
using COPaymentGateWay.Core;
using COPaymentGateWay.Core.Interfaces;
using COPaymentGateWay.Core.Models;

namespace COPaymentGateWay.Infrastructure.PaymentsRepo.Config
{
    public class PaymentsTableConfig : ITableConfig
    {
        private string _tableName;
        private KeyDetails _hashKey;
        private KeyDetails _rangeKey;

        public PaymentsTableConfig()
        {
            _tableName = "Payments";
            _hashKey = new KeyDetails()
            {
                AttributeType = ScalarAttributeType.S,
                ColumnName = "Identifier"
            };
            _rangeKey = null;

        }

        public string TableName { get { return _tableName; } }
        public KeyDetails HashKey { get { return _hashKey; } }
        public KeyDetails RangeKey { get { return _rangeKey; } }
    }
}
