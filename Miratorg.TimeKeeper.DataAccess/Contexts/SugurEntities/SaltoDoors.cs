using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class SaltoDoors
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ExtDoorId { get; set; }
        public string Description { get; set; }
        public bool? IsOnline { get; set; }
    }
}
