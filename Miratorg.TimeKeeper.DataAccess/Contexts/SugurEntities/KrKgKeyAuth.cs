using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class KrKgKeyAuth
    {
        public int Id { get; set; }
        public int? KeyrackId { get; set; }
        public int? Addr { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? KeysId { get; set; }
        public int? TzId { get; set; }
        public int? RuleId { get; set; }
        public bool? ConfirmOpr { get; set; }
        public bool? ConfirmFr { get; set; }
    }
}
