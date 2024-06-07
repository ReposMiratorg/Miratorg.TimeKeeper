using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Paylog
    {
        public int Id { get; set; }
        public DateTime? Logtime { get; set; }
        public DateTime? Optime { get; set; }
        public int? Userid { get; set; }
        public int? Apid { get; set; }
        public int? Accid { get; set; }
        public string Optype { get; set; }
        public double? Oldvalue { get; set; }
        public double? Newvalue { get; set; }
    }
}
