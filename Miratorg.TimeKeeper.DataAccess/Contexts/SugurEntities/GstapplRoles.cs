using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class GstapplRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? CanEditRules { get; set; }
        public bool? CanMoveBack { get; set; }
    }
}
