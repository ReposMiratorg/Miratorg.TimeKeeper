using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class SaltoEventsCache
    {
        public int Id { get; set; }
        public DateTime? RecDateTime { get; set; }
        public int? OperationId { get; set; }
        public DateTime? EventDateTime { get; set; }
        public bool? IsExit { get; set; }
        public int? SubjType { get; set; }
        public string UserExtId { get; set; }
        public string DoorExtId { get; set; }
    }
}
