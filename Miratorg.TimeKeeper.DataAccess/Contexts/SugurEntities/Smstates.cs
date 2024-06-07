using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Smstates
    {
        public int Smid { get; set; }
        public int Stateid { get; set; }
        public string Name { get; set; }
        public int? Flags { get; set; }
        public int? OutputPort { get; set; }
        public int? OutputValue { get; set; }
    }
}
