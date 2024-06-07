using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Cctvservers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Ip { get; set; }
        public int? Port { get; set; }
        public string Login { get; set; }
        public string Passwd { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public bool? FgVideo { get; set; }
        public bool? FgPhoto { get; set; }
        public bool? FgSetGuestPhoto { get; set; }
        public string Url { get; set; }
        public int? FgMaxframewidth { get; set; }
        public int? FgMaxframeheight { get; set; }
        public int? FgMaxfps { get; set; }
        public int? FgPreseconds { get; set; }
        public int? FgPostseconds { get; set; }
        public string FgOnvifToken { get; set; }
        public string FgOnvifName { get; set; }
        public string FgOnvifType { get; set; }
        public DateTime? SsFrameDate { get; set; }
        public int? SsFrameDescriptorIdx { get; set; }
        public string Param4 { get; set; }
        public string Param5 { get; set; }
        public string Param6 { get; set; }
        public string Param7 { get; set; }
        public string Param8 { get; set; }
    }
}
