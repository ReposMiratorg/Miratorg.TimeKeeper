using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Alarmlog
    {
        public int Id { get; set; }
        public DateTime? Logtime { get; set; }
        public long Framets { get; set; }
        public int? Lineid { get; set; }
        public int? Newstate { get; set; }
    }
}
