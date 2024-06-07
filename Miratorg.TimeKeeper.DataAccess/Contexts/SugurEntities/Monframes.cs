using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Monframes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Monviewid { get; set; }
        public int? MainFrame { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
