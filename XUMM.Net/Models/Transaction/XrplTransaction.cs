﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using XUMM.Net.Enums;

namespace XUMM.Net.Models.Transaction
{
    public class XrplTransaction : XummPayloadTransactionBase
    {
        public XrplTransaction(XrplTransactionType transactionType, int fee) : base(transactionType.ToString())
        {
            Fee = fee.ToString();
        }

        [JsonPropertyName("Account")]
        public string? Account { get; set; }

        [JsonPropertyName("Fee")]
        public string Fee { get; }

        [JsonPropertyName("Sequence")]
        public int? Sequence { get; set; }

        [JsonPropertyName("AccountTxnID")]
        public string? AccountTxnId { get; set; }

        [JsonPropertyName("Flags")]
        public int? Flags { get; set; }

        [JsonPropertyName("LastLedgerSequence")]
        public int? LastLedgerSequence { get; set; }

        [JsonPropertyName("Memos")]
        public List<XrplTransactionMemoField>? Memos { get; set; }

        [JsonPropertyName("Signers")]
        public List<XrplTransactionSignerField>? Signers { get; set; }

        [JsonPropertyName("SourceTag")]
        public int? SourceTag { get; set; }

        [JsonPropertyName("SigningPubKey")]
        public string? SigningPubKey { get; set; }

        [JsonPropertyName("TicketSequence")]
        public int? TicketSequence { get; set; }

        [JsonPropertyName("TxnSignature")]
        public string? TxnSignature { get; set; }
    }
}