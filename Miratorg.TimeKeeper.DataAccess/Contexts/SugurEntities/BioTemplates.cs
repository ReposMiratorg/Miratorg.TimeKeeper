using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class BioTemplates
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string Type { get; set; }
        public byte[] Template { get; set; }
    }
}
