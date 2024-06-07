using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class OsdpDevices
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public int? Addr { get; set; }
        public int? Baudrate { get; set; }
        public int? DevId { get; set; }
        public int? IndprofileId { get; set; }
        public int? EncprofileId { get; set; }
        public string Checksum { get; set; }
    }
}
