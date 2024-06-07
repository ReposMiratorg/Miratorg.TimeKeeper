using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class OsdpEncprofile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? AesEnabled { get; set; }
        public byte[] AesKey { get; set; }
    }
}
