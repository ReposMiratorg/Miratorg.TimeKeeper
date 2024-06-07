using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Planfloors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Orderidx { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Scale { get; set; }
        public int? Color { get; set; }
        public bool? Keepbgar { get; set; }
    }
}
