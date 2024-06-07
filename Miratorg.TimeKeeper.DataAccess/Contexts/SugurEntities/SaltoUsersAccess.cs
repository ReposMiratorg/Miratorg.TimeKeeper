using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class SaltoUsersAccess
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string ExtUserId { get; set; }
        public string ExtZoneId { get; set; }
        public string ExtDoorId { get; set; }
        public int? TimeZoneTableId { get; set; }
    }
}
