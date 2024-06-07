using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class ParkingPasses
    {
        public int Id { get; set; }
        public int? TariffId { get; set; }
        public int? EmpId { get; set; }
        public int? EntryDevId { get; set; }
        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
