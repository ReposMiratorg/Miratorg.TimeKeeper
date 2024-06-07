using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class ParkingTariffPartDaytimes
    {
        public int Id { get; set; }
        public int? TariffPartId { get; set; }
        public int? DayNumber { get; set; }
        public int? BeginTime { get; set; }
        public int? EndTime { get; set; }
    }
}
