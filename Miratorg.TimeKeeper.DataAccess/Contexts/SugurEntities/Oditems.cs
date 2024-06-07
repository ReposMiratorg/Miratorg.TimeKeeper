using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Oditems
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public int? EmpId { get; set; }
        public int? TypeId { get; set; }
        public int Extsourceid { get; set; }
        public string Extid { get; set; }
    }
}
