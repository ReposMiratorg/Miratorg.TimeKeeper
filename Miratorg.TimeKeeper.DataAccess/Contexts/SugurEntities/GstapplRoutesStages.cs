using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class GstapplRoutesStages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RouteId { get; set; }
        public bool? NotifyCreator { get; set; }
        public bool? NotifyRoles { get; set; }
        public int? OrderIdx { get; set; }
    }
}
