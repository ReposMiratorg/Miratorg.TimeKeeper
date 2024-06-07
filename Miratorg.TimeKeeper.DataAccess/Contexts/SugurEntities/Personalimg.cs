using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Personalimg
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? GbId { get; set; }
        public int? OrderIdx { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
