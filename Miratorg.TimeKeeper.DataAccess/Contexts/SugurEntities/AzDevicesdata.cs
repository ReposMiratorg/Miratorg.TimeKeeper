using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class AzDevicesdata
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public int? Port { get; set; }
        public int? Userid { get; set; }
        public int? Fp0 { get; set; }
        public int? Fp1 { get; set; }
        public int? Fp2 { get; set; }
        public int? Fp3 { get; set; }
        public int? Fp4 { get; set; }
        public int? Fp5 { get; set; }
        public int? Fp6 { get; set; }
        public int? Fp7 { get; set; }
        public int? Fp8 { get; set; }
        public int? Fp9 { get; set; }
        public byte[] Codekey { get; set; }
        public int? Pwd { get; set; }
        public int? Attmode { get; set; }
        public string Name { get; set; }
        public bool? Dirtybit { get; set; }
    }
}
