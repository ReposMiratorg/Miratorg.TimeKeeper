using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class GstapplActionLog
    {
        public int Id { get; set; }
        public DateTime? Time { get; set; }
        public int ApplId { get; set; }
        public int StageId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
    }
}
