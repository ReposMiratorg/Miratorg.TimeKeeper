using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class BleRegtokens
    {
        public int Id { get; set; }
        public DateTime? Createdtime { get; set; }
        public DateTime? Regtime { get; set; }
        public int? Objtype { get; set; }
        public int? Objid { get; set; }
        public byte[] Token { get; set; }
    }
}
