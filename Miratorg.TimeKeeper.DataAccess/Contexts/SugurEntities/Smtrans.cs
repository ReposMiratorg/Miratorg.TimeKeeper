using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Smtrans
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Smid { get; set; }
        public int? SourceState { get; set; }
        public int? TargetState { get; set; }
        public string Type { get; set; }
        public int? Trtime { get; set; }
        public int? Timeparam { get; set; }
        public int? Port { get; set; }
        public int? Portvalue { get; set; }
    }
}
