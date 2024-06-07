using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class SaltoUsers
    {
        public int? Id { get; set; }
        public string ExtUserId { get; set; }
        public string AutoKeyEditRomcode { get; set; }
        public bool? IsBanned { get; set; }
        public bool? AuditOpenings { get; set; }
        public bool? CurrentKeyExists { get; set; }
        public int? CurrentKeyStatus { get; set; }
        public string CurrentRomCode { get; set; }
        public string PinCode { get; set; }
        public string Wiegandcode { get; set; }
        public int? CalendarId { get; set; }
        public bool? KeyExpirationDifferentFromUserExpiration { get; set; }
        public bool? KeyIsCancellableThroughBlackList { get; set; }
        public bool? KeyRevalidationUnitOfUpdatePeriod { get; set; }
        public int? KeyRevalidationUpdatePeriod { get; set; }
        public bool? LockdownEnabled { get; set; }
        public bool? Office { get; set; }
        public bool? OverrideLockdown { get; set; }
        public bool? PinEnabled { get; set; }
        public bool? UseAda { get; set; }
        public bool? UseAntiPassback { get; set; }
        public string UserActivation { get; set; }
        public string UserExpiration { get; set; }
        public bool? UserExpirationEnabled { get; set; }
    }
}
