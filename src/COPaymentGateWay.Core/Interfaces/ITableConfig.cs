﻿using COPaymentGateWay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPaymentGateWay.Core.Interfaces
{
    public interface ITableConfig
    {
        public string TableName { get; }

        public KeyDetails HashKey { get; }

        public KeyDetails RangeKey { get; }
    }
}
