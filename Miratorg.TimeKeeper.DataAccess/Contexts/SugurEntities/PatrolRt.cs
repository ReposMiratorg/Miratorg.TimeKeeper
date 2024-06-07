using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class PatrolRt
    {
        public int RuleId { get; set; }
        public int ApId { get; set; }
        public DateTime? LastLogTime { get; set; }
        public int? LastLogEmp { get; set; }
        public DateTime? LastViolationTime { get; set; }
    }
}
