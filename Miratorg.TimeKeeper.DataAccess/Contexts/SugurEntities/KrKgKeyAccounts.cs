using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class KrKgKeyAccounts
    {
        public int Id { get; set; }
        public int? KeyrackId { get; set; }
        public int? Addr { get; set; }
        public int? PersonalId { get; set; }
        public int? KeyAuthId { get; set; }
        public int? NameTextId { get; set; }
        public int? PhoneTextId { get; set; }
        public int? UserId { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }
}
