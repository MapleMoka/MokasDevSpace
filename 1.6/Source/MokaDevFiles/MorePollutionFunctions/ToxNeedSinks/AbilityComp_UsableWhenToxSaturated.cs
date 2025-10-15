using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class AbilityComp_UsableWhenToxSaturated : AbilityComp
    {
        public override bool GizmoDisabled(out string reason)
        {
            Need_Pollution needCheck = this.parent.pawn.needs.TryGetNeed<Need_Pollution>();
            if (needCheck == null)
            {
                reason = (string)"MokaDevSpace.AbilityNeedsPollutionNeed".Translate();
                return true;
            }
            if (needCheck != null) 
            {
                if (needCheck.CurLevel < 0.75f)
                {
                    reason = (string)"MokaDevSpace.AbilityNeedsPollutionLevel".Translate();
                    return true;
                }
            }
            return base.GizmoDisabled(out reason);
        }
    }
}
