using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Zones
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Capacitylimit { get; set; }
        public int? BusyoutAp { get; set; }
        public int? BusyoutPort { get; set; }
        public int? InfotabFtsn { get; set; }
        public bool? EvacrepWrk { get; set; }
        public bool? EvacrepEvac { get; set; }
    }
}
