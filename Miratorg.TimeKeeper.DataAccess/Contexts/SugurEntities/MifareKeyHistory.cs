using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class MifareKeyHistory
    {
        public int Id { get; set; }
        public DateTime? Ts { get; set; }
        public string KeyType { get; set; }
        public string KeyString { get; set; }
    }
}
