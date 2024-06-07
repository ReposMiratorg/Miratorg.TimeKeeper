using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Logbuffer
    {
        public int Id { get; set; }
        public int? Ap { get; set; }
        public int? BaseAddr { get; set; }
        public byte[] Data { get; set; }
    }
}
