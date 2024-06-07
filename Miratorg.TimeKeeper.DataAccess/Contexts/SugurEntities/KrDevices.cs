using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class KrDevices
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public int? KgSerialNumber { get; set; }
        public string KgIp { get; set; }
        public int? KgAddr { get; set; }
        public string KgRoomNumber { get; set; }
        public string KgReaderType { get; set; }
        public int? CamServerId { get; set; }
        public byte[] CamParam1 { get; set; }
    }
}
