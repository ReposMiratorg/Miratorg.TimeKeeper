using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class IndActions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Priority { get; set; }
        public string LedType { get; set; }
        public int? LedL1 { get; set; }
        public int? LedL2 { get; set; }
        public int? LedC1 { get; set; }
        public int? LedC2 { get; set; }
        public int? LedRepCount { get; set; }
        public string SndType { get; set; }
        public int? SndDigL1 { get; set; }
        public int? SndDigL2 { get; set; }
        public int? SndDigRepCount { get; set; }
        public int? SndWavId { get; set; }
        public int? SndWavRepCount { get; set; }
        public int? SndWavRepDelta { get; set; }
    }
}
