using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Telegramqueue
    {
        public int Id { get; set; }
        public DateTime? Pushtime { get; set; }
        public string ChatId { get; set; }
        public string Msgtext { get; set; }
    }
}
