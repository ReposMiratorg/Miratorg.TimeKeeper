using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class GstapplCar
    {
        public int Id { get; set; }
        public int? ApplId { get; set; }
        public string Lpnumber { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Comment { get; set; }
    }
}
