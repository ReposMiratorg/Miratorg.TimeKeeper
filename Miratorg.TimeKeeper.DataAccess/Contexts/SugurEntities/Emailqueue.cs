using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Emailqueue
    {
        public int Id { get; set; }
        public DateTime? Pushtime { get; set; }
        public string ToCsv { get; set; }
        public string CcCsv { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
