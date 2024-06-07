using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Odtypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CalculationType { get; set; }
        public int? Priority { get; set; }
    }
}
