using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Badgeitems
    {
        public int Id { get; set; }
        public int? Badgeid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Xpos { get; set; }
        public int? Ypos { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string FontName { get; set; }
        public bool? FontBold { get; set; }
        public bool? FontItalic { get; set; }
        public int? FontMin { get; set; }
        public int? FontDef { get; set; }
        public int? FontColorR { get; set; }
        public int? FontColorB { get; set; }
        public int? FontColorG { get; set; }
        public bool? Eba { get; set; }
        public string Align { get; set; }
        public int? Rotate { get; set; }
        public string BarcodeType { get; set; }
        public bool? PhotoCircle { get; set; }
        public string Param1 { get; set; }
        public byte[] Bindata { get; set; }
        public string ImgType { get; set; }
        public string ImgName { get; set; }
        public string CondType { get; set; }
        public string CondSearchFor { get; set; }
        public string CondSearchWhere { get; set; }
    }
}
