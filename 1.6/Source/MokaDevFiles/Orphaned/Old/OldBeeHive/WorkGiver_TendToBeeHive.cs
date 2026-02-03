using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;

namespace MFM
{
    internal class WorkGiver_TendToBeeHive : WorkGiver_Scanner
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(MFM_DefOf.Moka_BeeHive);

        public override PathEndMode PathEndMode => PathEndMode.Touch;

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!(t is Building_BeeHive apiary) || !apiary.needTend || t.IsBurning() || t.IsForbidden(pawn))
                return false;
            LocalTargetInfo target = (LocalTargetInfo)t;
            return pawn.CanReserve(target, ignoreOtherReservations: forced);
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false) => new Job(MFM_DefOf.Moka_TendBeeHive, (LocalTargetInfo)t);
    }
}
