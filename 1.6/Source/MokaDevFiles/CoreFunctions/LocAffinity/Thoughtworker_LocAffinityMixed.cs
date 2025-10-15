using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class Thoughtworker_LocAffinityMixed : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (!Util_LocAffinity.SharesLocAffinityMixedGene(p, otherPawn) || !RelationsUtility.PawnsKnowEachOther(p, otherPawn))
            {
                return false;
            }
            if (otherPawn.HasRealGene(MCM_DefOf.Moka_LocAffinityMild))
            {
                return ThoughtState.ActiveAtStage(1);
            }
            if (otherPawn.HasRealGene(MCM_DefOf.Moka_LocAffinityStrong))
            {
                return ThoughtState.ActiveAtStage(1);
            }
            return false;
        }
    }
}