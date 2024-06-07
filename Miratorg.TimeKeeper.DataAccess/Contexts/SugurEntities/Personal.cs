﻿using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Personal
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Pos { get; set; }
        public string Tabid { get; set; }
        public string Status { get; set; }
        public byte[] Codekey { get; set; }
        public DateTime? Codekeytime { get; set; }
        public string CodekeyDispFormat { get; set; }
        public byte[] MfUid { get; set; }
        public string SoaaKeyStatus { get; set; }
        public string SoaaUid { get; set; }
        public DateTime? Exptime { get; set; }
        public DateTime? Createdtime { get; set; }
        public DateTime? Firedtime { get; set; }
        public int? Badge { get; set; }
        public int? Badgeb { get; set; }
        public bool? Boolparam1 { get; set; }
        public bool? Boolparam2 { get; set; }
        public bool? Boolparam3 { get; set; }
        public bool? Boolparam4 { get; set; }
        public int? Locationzone { get; set; }
        public DateTime? Locationact { get; set; }
        public bool? ApbOn { get; set; }
        public bool? AplOn { get; set; }
        public DateTime? AplExptime { get; set; }
        public string Sideparam0 { get; set; }
        public string Sideparam1 { get; set; }
        public string Sideparam2 { get; set; }
        public string Sideparam3 { get; set; }
        public string Sideparam4 { get; set; }
        public string Sideparam5 { get; set; }
        public bool? NtfyPassEnabled { get; set; }
        public string NtfyType { get; set; }
        public DateTime? NtfyStartdate { get; set; }
        public DateTime? NtfyEnddate { get; set; }
        public string NtfyPassText { get; set; }
        public bool? NtfyPayEnabled { get; set; }
        public string NtfyPayText { get; set; }
        public bool? NtfyGstapplEnabled { get; set; }
        public string NtfyGstapplText { get; set; }
        public bool? NtfyPayDecEnabled { get; set; }
        public string NtfyPayDecText { get; set; }
        public double? NtfyPayDecThr { get; set; }
        public DateTime? NtfyLastTime { get; set; }
        public int? NtfyLastAp { get; set; }
        public int? NtfyLastDir { get; set; }
        public bool? NtfyLastSent { get; set; }
        public string SmsTargetnumber { get; set; }
        public string Email { get; set; }
        public string EmailSubject { get; set; }
        public string TelegramChatid { get; set; }
        public int? LastpassAp { get; set; }
        public bool? AdEnabled { get; set; }
        public int? AdDomainId { get; set; }
        public string AdUserDn { get; set; }
        public int? AdSyncPending { get; set; }
        public int Extsourceid { get; set; }
        public string Extid { get; set; }
        public bool? UserEnabled { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserGstapplRange { get; set; }
        public bool? UserGstapplCreate { get; set; }
        public bool? UserGstapplModify { get; set; }
        public bool? UserDepsrestriction { get; set; }
        public string UserMonviewPolicy { get; set; }
        public bool? UserAprestriction { get; set; }
        public bool? UserFloorsrestriction { get; set; }
        public bool? UserReprestriction { get; set; }
        public bool? UserAlmrestriction { get; set; }
        public string UserExitpassword { get; set; }
        public bool? UserExitpasswordUsed { get; set; }
        public bool? UserTEditplans { get; set; }
        public bool? UserTCardlogin { get; set; }
        public bool? UserChownpass { get; set; }
        public bool? UserWritedb { get; set; }
        public bool? UserTHw { get; set; }
        public bool? UserTMon { get; set; }
        public bool? UserTMonCntlap { get; set; }
        public bool? UserTMonCntlalm { get; set; }
        public bool? UserTMonAllowanonpass { get; set; }
        public bool? UserTMonAllowpass { get; set; }
        public bool? UserTMonAllowauthpass { get; set; }
        public bool? UserTFace { get; set; }
        public bool? UserTPayacc { get; set; }
        public bool? UserTPersonal { get; set; }
        public bool? UserTPersonalAdd { get; set; }
        public bool? UserTPersonalDel { get; set; }
        public bool? UserTPersonalEdit { get; set; }
        public bool? UserTPersonalBadges { get; set; }
        public bool? UserTPersonalAcc { get; set; }
        public bool? UserTPersonalSms { get; set; }
        public bool? UserTPersonalAccess { get; set; }
        public bool? UserTPersonalSetzone { get; set; }
        public bool? UserTPayinc { get; set; }
        public bool? UserTPayaccmng { get; set; }
        public bool? UserTPaydesk { get; set; }
        public bool? UserTPaydeskAccinc { get; set; }
        public bool? UserTPaydeskManualselect { get; set; }
        public bool? UserTPaydesklite { get; set; }
        public bool? UserTPaydeskliteManualsel { get; set; }
        public bool? UserTRules { get; set; }
        public bool? UserTPaymenu { get; set; }
        public bool? UserTAutopark { get; set; }
        public bool? UserTGuests { get; set; }
        public bool? UserTGuestsEdit { get; set; }
        public bool? UserTGuestsDeletepd { get; set; }
        public bool? UserTEvents { get; set; }
        public bool? UserTAlarm { get; set; }
        public bool? UserTAlarmEditconf { get; set; }
        public bool? UserTAlarmCmd { get; set; }
        public bool? UserTArchive { get; set; }
        public bool? UserTReports { get; set; }
        public bool? UserTUsers { get; set; }
        public bool? UserLocalsettings { get; set; }
        public bool? UserTPlans { get; set; }
        public bool? UserCntlmodules { get; set; }
        public bool? UserTOd { get; set; }
        public bool? UserTParkingtariffs { get; set; }
        public bool? UserTParkingpay { get; set; }
        public bool? UserOif { get; set; }
        public bool? UserTNfcterminal { get; set; }
        public bool? UserTNfcterminalRegIn { get; set; }
        public bool? UserTNfcterminalRegOut { get; set; }
        public bool? UserTNfcterminalRegAuto { get; set; }
        public bool? UserTKeyguard { get; set; }
        public bool? UserApplsSeeCurrent { get; set; }
        public bool? UserApplsSeeAll { get; set; }
        public bool? UserApplsEditCurrent { get; set; }
        public bool? UserApplsEditAll { get; set; }
        public int? LastlprAp { get; set; }
        public int? LastlprDir { get; set; }
        public DateTime? LastlprTime { get; set; }
        public DateTime? IdleLastntfy { get; set; }
        public bool? UserTPersonalAd { get; set; }
        public bool? UserTSspilogin { get; set; }
        public bool? UserTMolockers { get; set; }
        public bool? UserTUnlockMolockers { get; set; }
    }
}
