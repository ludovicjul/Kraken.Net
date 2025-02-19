﻿namespace Kraken.Net.Objects
{
    /// <summary>
    /// Withdraw info
    /// </summary>
    public class KrakenWithdrawInfo
    {
        /// <summary>
        /// Method that will be used
        /// </summary>
        public string Method { get; set; } = string.Empty;
        /// <summary>
        /// Limit to what can be withdrawn right now
        /// </summary>
        public decimal Limit { get; set; }
        /// <summary>
        /// Fee that will be paid
        /// </summary>
        public decimal Fee { get; set; }
    }
}
