using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class ParkingPassCosts
    {
        public int Id { get; set; }
        public int? PassId { get; set; }
        public int? TariffPartId { get; set; }
        public double? Cost { get; set; }
    }
}
