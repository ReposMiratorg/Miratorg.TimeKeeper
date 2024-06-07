using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class KrKeys
    {
        public int Id { get; set; }
        public int? KeyrackId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Alarmlines { get; set; }
        public string KgParents { get; set; }
        public int? KgAddr { get; set; }
        public string KgRoomNumber { get; set; }
        public int? KgBlockNumber { get; set; }
        public int? KgCellNumber { get; set; }
        public int? KgReturnTimeHour { get; set; }
        public int? KgReturnTimeMin { get; set; }
        public string KgReturnDelayType { get; set; }
        public int? KgReturnDelay { get; set; }
        public int? KgKeylistTextIndex { get; set; }
        public string KgKeyIbutton { get; set; }
        public bool? KgKeyListExtension { get; set; }
        public int? KgExtensionParentId { get; set; }
        public int? KgDelayHour { get; set; }
        public int? KgDelayMin { get; set; }
        public bool? KgKeyNeedConfirm { get; set; }
    }
}
