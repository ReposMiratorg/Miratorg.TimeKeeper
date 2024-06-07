using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Gstappl
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? OwnerId { get; set; }
        public DateTime? Createdtime { get; set; }
        public int? CometoId { get; set; }
        public DateTime? PeriodBegin { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public string Comment { get; set; }
        public string Sideparam0 { get; set; }
        public string Sideparam1 { get; set; }
        public string Sideparam2 { get; set; }
        public string Sideparam3 { get; set; }
    }
}
