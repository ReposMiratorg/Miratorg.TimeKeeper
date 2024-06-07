using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Paymenus
    {
        public int Id { get; set; }
        public string Extid { get; set; }
        public string Name { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Color { get; set; }
    }
}
