﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopLite.Core.Entities
{
    public class QueueStatus
    {
        [JsonProperty]
        public long MessageCount { get; set; }
    }
}
