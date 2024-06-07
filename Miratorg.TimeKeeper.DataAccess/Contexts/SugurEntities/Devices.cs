using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Devices
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? OrderIdx { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int? Area { get; set; }
        public int? TimezoneId { get; set; }
        public string Linktype { get; set; }
        public byte[] Linkparam1 { get; set; }
        public byte[] Linkparam2 { get; set; }
        public byte[] Linkparam3 { get; set; }
        public byte[] Linkparam4 { get; set; }
        public bool? Disabled { get; set; }
        public string Hwconf { get; set; }
        public bool? Controlsperimeter { get; set; }
        public int? Zonea { get; set; }
        public int? Zoneb { get; set; }
        public int? CamaServerId { get; set; }
        public byte[] CamaParam1 { get; set; }
        public byte[] CamaParam2 { get; set; }
        public byte[] CamaParam3 { get; set; }
        public byte[] CamaParam4 { get; set; }
        public bool? CamaLpr { get; set; }
        public bool CamaFacevEnabled { get; set; }
        public bool CamaFaceiEnabled { get; set; }
        public int? CambServerId { get; set; }
        public byte[] CambParam1 { get; set; }
        public byte[] CambParam2 { get; set; }
        public byte[] CambParam3 { get; set; }
        public byte[] CambParam4 { get; set; }
        public bool? CambLpr { get; set; }
        public bool CambFacevEnabled { get; set; }
        public bool CambFaceiEnabled { get; set; }
        public string Pinmagica { get; set; }
        public string Pinmagicb { get; set; }
        public int? Gatesm { get; set; }
        public bool? GuestCloseonexit { get; set; }
        public string BioaType { get; set; }
        public string BioaIp { get; set; }
        public int? BioaPort { get; set; }
        public string BiobType { get; set; }
        public string BiobIp { get; set; }
        public int? BiobPort { get; set; }
        public int? TrueipLasteventId { get; set; }
        public bool? Sms { get; set; }
        public int NfcExptimeCheck { get; set; }
        public bool? AplOn { get; set; }
        public bool? AplTarget { get; set; }
        public int Extsourceid { get; set; }
        public string Extid { get; set; }
        public byte[] Linkparam5 { get; set; }
    }
}
