using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Planobjects
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? Floorid { get; set; }
        public int? Apid { get; set; }
        public int? Zoneid { get; set; }
        public int? Camid { get; set; }
        public int? Alarmid { get; set; }
        public int? TargetFloor { get; set; }
        public int? CamServerId { get; set; }
        public byte[] CamParam1 { get; set; }
        public byte[] CamParam2 { get; set; }
        public byte[] CamParam3 { get; set; }
        public string Xpos { get; set; }
        public string Ypos { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public double? Angle { get; set; }
        public int? Zpos { get; set; }
        public int? LineColor { get; set; }
        public string LineWidth { get; set; }
        public string Text { get; set; }
        public string TextFontsize { get; set; }
        public int? TextFontcolor { get; set; }
        public string TextHalign { get; set; }
        public string TextValign { get; set; }
        public bool? TextWordwrap { get; set; }
        public int? FillColor { get; set; }
        public string RenderingStyle { get; set; }
        public bool? AlarmColorind { get; set; }
        public bool? Autotext { get; set; }
        public string AlarmIcon { get; set; }
        public byte[] Param1 { get; set; }
    }
}
