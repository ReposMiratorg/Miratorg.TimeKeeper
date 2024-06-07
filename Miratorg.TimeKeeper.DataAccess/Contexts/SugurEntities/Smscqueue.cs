using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Smscqueue
    {
        public int Id { get; set; }
        public DateTime? Pushtime { get; set; }
        public string Msgtype { get; set; }
        public string Targetnumber { get; set; }
        public string Msgtext { get; set; }
    }
}
