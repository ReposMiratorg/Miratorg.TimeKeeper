using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Userlog
    {
        public int Id { get; set; }
        public DateTime? Logtime { get; set; }
        public int? Userid { get; set; }
        public string Clientip { get; set; }
        public int? Apid { get; set; }
        public int? Objid { get; set; }
        public string Text { get; set; }
    }
}
