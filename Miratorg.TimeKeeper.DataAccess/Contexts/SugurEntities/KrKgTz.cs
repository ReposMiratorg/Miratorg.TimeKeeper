using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class KrKgTz
    {
        public int Id { get; set; }
        public int? KeyrackId { get; set; }
        public int? Addr { get; set; }
        public string Type { get; set; }
        public string IntervalType { get; set; }
        public int? IntervalStartHour { get; set; }
        public int? IntervalStartMin { get; set; }
        public int? IntervalEndHour { get; set; }
        public int? IntervalEndMin { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
