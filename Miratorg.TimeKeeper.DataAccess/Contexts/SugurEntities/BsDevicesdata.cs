using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class BsDevicesdata
    {
        public int Id { get; set; }
        public byte[] Ip { get; set; }
        public int? Port { get; set; }
        public byte[] Codekey { get; set; }
        public string Name { get; set; }
        public string BsType { get; set; }
        public int? TemplateId { get; set; }
        public bool? Dirtybit { get; set; }
    }
}
