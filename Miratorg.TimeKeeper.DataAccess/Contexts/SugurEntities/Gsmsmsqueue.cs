using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Gsmsmsqueue
    {
        public int Id { get; set; }
        public DateTime? Pushtime { get; set; }
        public string Targetnumber { get; set; }
        public string Smstext { get; set; }
    }
}
