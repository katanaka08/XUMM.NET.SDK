﻿using System.Text.Json.Serialization;
using XUMM.NET.SDK.Enums;

namespace XUMM.NET.SDK.Models.Payload.XRPL
{
    public class XrplPaymentTransaction : XrplTransaction
    {
        /// <summary>
        /// A Payment transaction represents a transfer of value from one account to another. (Depending on the path taken, this can involve additional exchanges of value, which occur atomically.)
        /// </summary>
        /// <param name="destination">The unique address of the account receiving the payment.</param>
        /// <param name="destinationTag">(Optional) Arbitrary tag that identifies the reason for the payment to the destination, or a hosted recipient to pay.</param>
        /// <param name="fee">Integer amount of XRP, in drops, to be destroyed as a cost for distributing this transaction to the network.</param>
        public XrplPaymentTransaction(string destination, int? destinationTag, int fee) : base(XrplTransactionType.Payment, fee)
        {
            Destination = destination;
            DestinationTag = destinationTag;
        }

        /// <summary>
        /// The unique address of the account receiving the payment.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// (Optional) Arbitrary tag that identifies the reason for the payment to the destination, or a hosted recipient to pay.
        /// </summary>
        public int? DestinationTag { get; set; }

        /// <summary>
        /// (Optional) Arbitrary 256-bit hash representing a specific reason or identifier for this payment.
        /// </summary>
        [JsonPropertyName("InvoiceID")]
        public string? InvoiceId { get; set; }

        /// <summary>
        /// The amount of currency to deliver. For non-XRP amounts, the nested field names MUST be lower-case. If the tfPartialPayment flag is set, deliver up to this amount instead.
        /// </summary>
        [JsonPropertyName("Amount")]
        public XrplTransactionCurrencyAmount? Amount { get; set; }

        /// <summary>
        /// (Optional) Highest amount of source currency this transaction is allowed to cost, including transfer fees, exchange rates, and slippage.
        /// </summary>
        [JsonPropertyName("SendMax")]
        public XrplTransactionCurrencyAmount? SendMax { get; set; }

        /// <summary>
        /// (Optional) Minimum amount of destination currency this transaction should deliver. Only valid if this is a partial payment.
        /// </summary>
        [JsonPropertyName("DeliverMin")]
        public XrplTransactionCurrencyAmount? DeliverMin { get; set; }
    }
}