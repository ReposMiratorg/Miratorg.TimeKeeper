using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class ParkingTariffParts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TariffId { get; set; }
        public int? T1 { get; set; }
        public int? T2 { get; set; }
        public double? S1 { get; set; }
        public double? S2 { get; set; }
        public double? S3 { get; set; }
        public int? D1 { get; set; }
        public int? D2 { get; set; }
        public int? D3 { get; set; }
    }
}
