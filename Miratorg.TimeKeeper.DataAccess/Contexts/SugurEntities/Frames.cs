using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Frames
    {
        public int Id { get; set; }
        public int? Cameraid { get; set; }
        public long? Timestamp { get; set; }
        public byte[] Raster { get; set; }
    }
}
