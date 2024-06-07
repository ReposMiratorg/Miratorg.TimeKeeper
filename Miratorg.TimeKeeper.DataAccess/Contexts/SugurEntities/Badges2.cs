using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Badges2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public byte[] Binbackground { get; set; }
    }
}
