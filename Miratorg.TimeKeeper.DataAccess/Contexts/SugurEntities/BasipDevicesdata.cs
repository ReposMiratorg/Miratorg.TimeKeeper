using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class BasipDevicesdata
    {
        public int Id { get; set; }
        public int? DeviceId { get; set; }
        public int? CardNumber { get; set; }
        public bool? Dirtybit { get; set; }
    }
}
