using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class GstapplPeople
    {
        public int Id { get; set; }
        public int? ApplId { get; set; }
        public string Type { get; set; }
        public int? EmpId { get; set; }
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Doctype { get; set; }
        public string Docname { get; set; }
        public string Docnumber { get; set; }
        public string Comment { get; set; }
    }
}
