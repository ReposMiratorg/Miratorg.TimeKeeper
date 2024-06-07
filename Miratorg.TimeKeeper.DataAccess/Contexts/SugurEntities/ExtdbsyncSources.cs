using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class ExtdbsyncSources
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int Period { get; set; }
        public string SqlOdbcstring { get; set; }
        public string SqlExtdbver { get; set; }
        public bool? SqlLogUsepass { get; set; }
        public bool SqlLogUsedeny { get; set; }
        public int SqlLogUsenoid { get; set; }
        public int SqlLogLastid { get; set; }
        public string DeptsDelimiter { get; set; }
        public string RulesDelimiter { get; set; }
        public string KeysDelimiter { get; set; }
        public int AdDomainId { get; set; }
        public string AdPath { get; set; }
        public int AdSyncAccdisabled { get; set; }
        public int AdSyncDepartment { get; set; }
        public bool AdSyncTabid { get; set; }
        public int OnecServerId { get; set; }
        public string OnecOrg { get; set; }
        public int SyncPhotos { get; set; }
        public string LdapUrl { get; set; }
        public string LdapAuthHandle { get; set; }
        public string LdapAuthPass { get; set; }
        public bool? LdapForceV3 { get; set; }
        public string DelPolicy { get; set; }
        public string DelDep { get; set; }
        public bool? DelSetexptime { get; set; }
        public bool? DelClrdevbindings { get; set; }
        public string ServsyncPeeruuid { get; set; }
        public int? ServsyncLogsOutLastid { get; set; }
        public int? ServsyncLogsInLastid { get; set; }
        public bool? ServsyncDevicesOut { get; set; }
        public bool? ServsyncDevicesIn { get; set; }
        public bool? ServsyncPersonalOut { get; set; }
        public bool? ServsyncPersonalIn { get; set; }
        public bool? ServsyncPhotoOut { get; set; }
        public bool? ServsyncPhotoIn { get; set; }
        public bool? ServsyncAccessrulesOut { get; set; }
        public bool? ServsyncAccessrulesIn { get; set; }
        public bool? ServsyncRulebindingsOut { get; set; }
        public bool? ServsyncRulebindingsIn { get; set; }
        public bool? ServsyncLogsOut { get; set; }
        public bool? ServsyncLogsIn { get; set; }
        public bool? ServsyncOfflineEnabled { get; set; }
        public string ServsyncOfflineExchangeDir { get; set; }
        public int? ServsyncOfflinePeriod { get; set; }
        public string ServsyncOfflineLastTs { get; set; }
        public bool? ServsyncOnlineEnabled { get; set; }
        public string ServsyncOnlineMode { get; set; }
        public string ServsyncOnlinePeerhost { get; set; }
        public int? ServsyncOnlinePeerport { get; set; }
        public int? ServsyncOnlinePeriod { get; set; }
        public string ServsyncKey { get; set; }
        public string ServsyncStatus { get; set; }
        public string ServsyncStatusDetails { get; set; }
    }
}
