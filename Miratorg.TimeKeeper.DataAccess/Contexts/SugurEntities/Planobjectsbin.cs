using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Planobjectsbin
    {
        public int Id { get; set; }
        public int? Floorid { get; set; }
        public int? Objid { get; set; }
        public byte[] Bindata { get; set; }
        public int? BindataTs { get; set; }
    }
}
