using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Paylogdetails
    {
        public int Id { get; set; }
        public int? Logid { get; set; }
        public int? Itemid { get; set; }
        public double? Costvalue { get; set; }
        public double? Countvalue { get; set; }
    }
}
