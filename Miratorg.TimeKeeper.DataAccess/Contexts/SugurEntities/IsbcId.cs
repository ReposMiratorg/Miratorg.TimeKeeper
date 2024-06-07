using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class IsbcId
    {
        public int Id { get; set; }
        public string ActivationCode { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public int? IssuedEmpId { get; set; }
        public DateTime? IssuedTime { get; set; }
    }
}
