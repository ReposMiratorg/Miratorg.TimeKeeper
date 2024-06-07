using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Alarmlines
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Type { get; set; }
        public int? OrderIdx { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Linetype { get; set; }
        public int? Ap { get; set; }
        public int? Zone { get; set; }
        public string Spnxtype { get; set; }
        public string Rubezhtype { get; set; }
        public int? CamServerId { get; set; }
        public byte[] CamParam1 { get; set; }
        public byte[] CamParam2 { get; set; }
        public byte[] CamParam3 { get; set; }
        public byte[] CamParam4 { get; set; }
    }
}
