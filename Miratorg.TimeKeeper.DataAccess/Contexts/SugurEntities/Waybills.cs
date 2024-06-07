using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Waybills
    {
        public int Id { get; set; }
        public int? Num { get; set; }
        public int? AutoId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? Datecre { get; set; }
        public DateTime? Dateact { get; set; }
        public DateTime? Datefin { get; set; }
        public string Comment { get; set; }
        public string Type { get; set; }
    }
}
