using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class PersonalKeys
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public byte[] Codekey { get; set; }
        public DateTime? Codekeytime { get; set; }
        public string CodekeyDispFormat { get; set; }
        public byte[] MfUid { get; set; }
        public DateTime? Exptime { get; set; }
    }
}
