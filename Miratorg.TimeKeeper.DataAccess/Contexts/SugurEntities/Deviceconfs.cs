using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Deviceconfs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Confdata { get; set; }
        public int? Iocount { get; set; }
        public byte[] Iorec { get; set; }
        public int? Gatesm { get; set; }
    }
}
