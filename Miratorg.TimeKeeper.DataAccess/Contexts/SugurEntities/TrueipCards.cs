using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class TrueipCards
    {
        public int Id { get; set; }
        public int? DevId { get; set; }
        public int? RecNo { get; set; }
        public byte[] CardNo { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string VtoPosition { get; set; }
        public int? CardType { get; set; }
        public int? CardStatus { get; set; }
    }
}
