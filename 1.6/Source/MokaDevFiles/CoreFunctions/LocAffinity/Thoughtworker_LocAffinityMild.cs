using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class Thoughtworker_LocAffinityMild : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (!Util_LocAffinity.SharesLocAffinityMildGene(p, otherPawn) || !RelationsUtility.PawnsKnowEachOther(p, otherPawn))
            {
                return false;
            }
            if (otherPawn.HasRealGene(MCM_DefOf.Moka_LocAffinityMild))
            {
                return ThoughtState.ActiveAtStage(1);
            }
            return false;
        }
    }
}