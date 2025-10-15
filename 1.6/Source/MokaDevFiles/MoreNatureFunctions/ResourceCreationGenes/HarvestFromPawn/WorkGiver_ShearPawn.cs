using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MFM
{
    public class WorkGiver_HarvestFromPawn : WorkGiver_GatherAnimalBodyResources
    {
        protected override JobDef JobDef => MFM_Produce_DefOf.Moka_ShearPawn;

        public override bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            List<Pawn> pawnList = pawn.Map.mapPawns.SpawnedPawnsInFaction(pawn.Faction);
            for (int index = 0; index < pawnList.Count; ++index)
            {
                if (pawnList[index].RaceProps.Humanlike)
                {
                    CompHasGatherableBodyResource comp = this.GetComp(pawnList[index]);
                    if (comp != null && comp.ActiveAndFull)
                        return false;
                }
            }
            return true;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!(t is Pawn pawn1) || !pawn1.RaceProps.Humanlike)
                return false;
            CompHasGatherableBodyResource comp = this.GetComp(pawn1);
            return comp != null && comp.ActiveAndFull && !pawn1.Downed && (pawn1.roping == null || !pawn1.roping.IsRopedByPawn) && pawn1.CanCasuallyInteractNow() && pawn.CanReserve((LocalTargetInfo)(Thing)pawn1, ignoreOtherReservations: forced);
        }

        protected override CompHasGatherableBodyResource GetComp(Pawn animal) => (CompHasGatherableBodyResource)ThingCompUtility.TryGetComp<Comp_HarvestFromGene>((Thing)animal);
    }
}
