using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Payacctypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unitname { get; set; }
        public double? Valuestep { get; set; }
        public double? Credit { get; set; }
        public bool? CreateByDefault { get; set; }
    }
}
