using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class KrKgHolidays
    {
        public int Id { get; set; }
        public int? KeyrackId { get; set; }
        public int? Addr { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
