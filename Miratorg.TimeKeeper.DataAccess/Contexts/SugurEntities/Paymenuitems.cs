using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Paymenuitems
    {
        public int Id { get; set; }
        public string Extid { get; set; }
        public int? Menuid { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public string Number { get; set; }
        public string Unitname { get; set; }
        public string CostType { get; set; }
        public string CostParam1 { get; set; }
        public int? IncaccTypeid { get; set; }
        public double? IncaccValue { get; set; }
        public int? RestrTime1 { get; set; }
        public int? RestrTime2 { get; set; }
        public string RestrCountType { get; set; }
        public string RestrCountParam1 { get; set; }
        public bool? RestrDay1 { get; set; }
        public bool? RestrDay2 { get; set; }
        public bool? RestrDay3 { get; set; }
        public bool? RestrDay4 { get; set; }
        public bool? RestrDay5 { get; set; }
        public bool? RestrDay6 { get; set; }
        public bool? RestrDay7 { get; set; }
        public int? Color { get; set; }
    }
}
