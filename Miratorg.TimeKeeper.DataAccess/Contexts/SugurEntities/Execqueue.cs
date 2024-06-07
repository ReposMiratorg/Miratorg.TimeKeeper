using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Execqueue
    {
        public int Id { get; set; }
        public DateTime? Pushtime { get; set; }
        public string Cmd { get; set; }
    }
}
