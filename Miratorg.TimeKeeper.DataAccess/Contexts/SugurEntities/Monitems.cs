using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Monitems
    {
        public int Id { get; set; }
        public int? Monframeid { get; set; }
        public string Type { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string EvsourceType { get; set; }
        public int? EvsourceItemId { get; set; }
        public int? EvsourceFilter { get; set; }
        public string VideoSource { get; set; }
        public int? VideochannelServerId { get; set; }
        public byte[] VideochannelParam1 { get; set; }
        public byte[] VideochannelParam2 { get; set; }
        public byte[] VideochannelParam3 { get; set; }
        public string TpFormat { get; set; }
        public int? FontSize { get; set; }
        public int? OprPanel { get; set; }
        public int? RedWarningEvents { get; set; }
        public int? AssociatedEmp { get; set; }
        public int? ApRestricted { get; set; }
        public bool? AutohideEnabled { get; set; }
        public int? AutohideTimer { get; set; }
        public bool? ProcessHtml { get; set; }
        public byte[] Bindata { get; set; }
        public int? BindataTs { get; set; }
        public string CondType { get; set; }
        public string CondSearchFor { get; set; }
        public string CondSearchWhere { get; set; }
        public int? Delay { get; set; }
        public int? Flags { get; set; }
        public int? AlarmMatrixWidth { get; set; }
        public string ActionType { get; set; }
        public string Cmd { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string ImageType { get; set; }
        public string ImagePersonalimgName { get; set; }
    }
}
