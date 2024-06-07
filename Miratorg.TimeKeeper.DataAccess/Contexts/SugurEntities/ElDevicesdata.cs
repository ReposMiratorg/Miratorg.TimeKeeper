using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class ElDevicesdata
    {
        public int Id { get; set; }
        public byte[] Host { get; set; }
        public int? Port { get; set; }
        public byte[] Codekey { get; set; }
        public int? TemplateId { get; set; }
    }
}
