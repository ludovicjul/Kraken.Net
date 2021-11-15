﻿using System;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Kraken.Net.Objects
{
    /// <summary>
    /// Placed order info
    /// </summary>
    public class KrakenPlacedOrder: ICommonOrderId
    {
        /// <summary>
        /// Order ids
        /// </summary>
        [JsonProperty("txid")]
        public IEnumerable<string> OrderIds { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Descriptions
        /// </summary>
        [JsonProperty("descr")]
        public KrakenPlacedOrderDescription Descriptions { get; set; } = default!;

        string ICommonOrderId.CommonId => OrderIds.First();
    }

    /// <summary>
    /// Order descriptions
    /// </summary>
    public class KrakenPlacedOrderDescription
    {
        /// <summary>
        /// Order description
        /// </summary>
        [JsonProperty("order")]
        public string OrderDescription { get; set; } = string.Empty;
        /// <summary>
        /// Close order description
        /// </summary>
        [JsonProperty("close")]
        public string CloseOrderDescription { get; set; } = string.Empty;
    }
}
