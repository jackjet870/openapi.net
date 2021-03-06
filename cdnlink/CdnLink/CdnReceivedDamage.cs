﻿using CarDeliveryNetwork.Api.Data;

namespace CdnLink
{
    public partial class CdnReceivedDamage
    {
        public CdnReceivedDamage(DamageItem damage, string damageAt)
            : this()
        {
            CdnDamageId = damage.Id;
            AreaCode = damage.Area.Code;
            AreaDescription = damage.Area.Description;
            SeverityCode = damage.Severity.Code;
            SeverityDescription = damage.Severity.Description;
            TypeCode = damage.Type.Code;
            TypeDescription = damage.Type.Description;
            DamageAt = damageAt;
        }
    }
}
