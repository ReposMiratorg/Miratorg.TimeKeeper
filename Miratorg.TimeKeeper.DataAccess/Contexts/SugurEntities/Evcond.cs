using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Evcond
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Param1 { get; set; }
        public byte[] Param2 { get; set; }
        public DateTime? Lastfiretime { get; set; }
        public byte[] Param3 { get; set; }
    }
}
