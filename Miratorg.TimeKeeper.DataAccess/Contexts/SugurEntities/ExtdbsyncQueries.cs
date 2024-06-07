using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public  class ExtdbsyncQueries
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Filter { get; set; }
    }
}
