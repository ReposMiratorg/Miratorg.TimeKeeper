using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class MiscMolockers
    {
        public int Id { get; set; }
        public int? OrderIdx { get; set; }
        public string Celltype { get; set; }
        public string Ip { get; set; }
        public int? Port { get; set; }
        public int? Branch { get; set; }
        public int? CfpSn { get; set; }
        public int? Cellnum { get; set; }
        public string Status { get; set; }
        public int? OccupiedBy { get; set; }
        public DateTime? OccupiedTime { get; set; }
        public int? OccupiedUseCount { get; set; }
        public int? Groupnum { get; set; }
        public int? ActualNumber { get; set; }
    }
}
