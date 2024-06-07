using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class MiscMolockersLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int? Cellid { get; set; }
        public int? Deviceid { get; set; }
        public int? Personalid { get; set; }
        public DateTime? Actiontime { get; set; }
    }
}
